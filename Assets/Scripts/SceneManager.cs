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
    
    private static DialogueNode _tempDialogueNode;
    private static Action _tempDialogueCallback; 

    static SceneManager()
    {
        _scenes.Add(Scenes.Credits, "Credits");
        _scenes.Add(Scenes.MainMenu, "Main Menu");
        _scenes.Add(Scenes.Level1, "Level 1");
        _scenes.Add(Scenes.DevPlayground, "DevPlayground");
        _scenes.Add(Scenes.Dialogue, "Dialogue");

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, mode) => SceneLoaded(scene);
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

    private static void _initializeDialogue(Scene s)
    {
        var renderer = Object.FindObjectOfType<DialogueRenderer>();
        renderer.Root = _tempDialogueNode;
        renderer.DialogueFinished.AddListener(scene => _tempDialogueCallback());
        renderer.Run();
        SceneLoaded -= _initializeDialogue;
    }

    public enum Scenes
    {
        MainMenu,
        Credits,
        Level1,
        DevPlayground,
        Dialogue
    }
}
