using System;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public static class SceneManager
{
    public static event Action<Scene> SceneLoaded = scene => { };
    
    private static readonly Dictionary<Scenes, string> _scenes = new Dictionary<Scenes, string>();
    private static readonly Dictionary<int, string> _levels = new Dictionary<int, string>();
    
    private static DialogueNode _tempDialogueNode;
    private static Action _tempDialogueCallback;

    private static GameObject _tempBossPrefab;
    private static Action _tempBossCallback;

    static SceneManager()
    {
        _scenes.Add(Scenes.Credits, "Credits");
        _scenes.Add(Scenes.MainMenu, "Main Menu");
        _scenes.Add(Scenes.DevPlayground, "DevPlayground");
        _scenes.Add(Scenes.Dialogue, "Dialogue");
        _scenes.Add(Scenes.Boss, "Boss");
        _scenes.Add(Scenes.Map, "Map");
        
        _levels.Add(1, "Level 1");

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, mode) => SceneLoaded(scene);
    }

    public static void LoadLevel(int level)
    {
        if (!_levels.TryGetValue(level, out var sceneName))
            throw new ArgumentException($"Invalid level {level}.");
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    
    public static void LoadScene(Scenes scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_scenes[scene]);
    }

    public static void LoadDialogueScene(DialogueNode node, Action cb)
    {
        _tempDialogueNode = node;
        _tempDialogueCallback = cb;
        SceneLoaded += _initializeDialogue;
        LoadScene(Scenes.Dialogue);
    }

    public static void LoadBossScene(GameObject boss, Action cb)
    {
        _tempBossPrefab = boss;
        _tempBossCallback = cb;
        SceneLoaded += _initializeBoss;
        LoadScene(Scenes.Boss);
    }

    private static void _initializeDialogue(Scene s)
    {
        var renderer = Object.FindObjectOfType<DialogueRenderer>();
        renderer.Root = _tempDialogueNode;
        renderer.DialogueFinished.AddListener(scene => _tempDialogueCallback());
        renderer.Run();
        SceneLoaded -= _initializeDialogue;
    }

    private static void _initializeBoss(Scene s)
    {
        var boss = Object.Instantiate(_tempBossPrefab, GameObject.FindWithTag("BossSpawner").transform.position, Quaternion.identity);
        boss.GetComponentInChildren<Boss.Boss>().OnDeath += _tempBossCallback;
        SceneLoaded -= _initializeBoss;
    }

    public enum Scenes
    {
        MainMenu,
        Credits,
        DevPlayground,
        Dialogue,
        Boss,
        Map
    }
}
