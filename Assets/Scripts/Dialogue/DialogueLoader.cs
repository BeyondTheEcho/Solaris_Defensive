using System.Collections;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;
using UnityEngine.Events;

public class DialogueLoader : MonoBehaviour
{
    [SerializeField] private DialogueNode startNode;
    [SerializeField] private UnityEvent callback;
    
    public void LoadDialogueScene()
    {
        SceneManager.LoadDialogueScene(startNode, ()=> callback.Invoke());
    }
}

