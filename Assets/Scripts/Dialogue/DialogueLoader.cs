using System.Collections;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;
using UnityEngine.Events;

public class DialogueLoader : MonoBehaviour
{
    [SerializeField] private DialogueNode startNode;
    [SerializeField] private int levelToLoad;
    
    public void LoadDialogueScene()
    {
        SceneManager.LoadDialogueScene(startNode, ()=> SceneManager.LoadLevel(levelToLoad));
    }
}

