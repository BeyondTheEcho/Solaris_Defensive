using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneManager
{
    private static readonly Dictionary<Scenes, string> _scenes = new Dictionary<Scenes, string>();
    
    static SceneManager()
    {
        _scenes.Add(Scenes.Credits, "Credits");
        _scenes.Add(Scenes.MainMenu, "Main Menu");
        _scenes.Add(Scenes.Level1, "Level 1");
    }
    
    public static void LoadScene(Scenes scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_scenes[scene]);
    }

    public enum Scenes
    {
        MainMenu,
        Credits,
        Level1
    }
}
