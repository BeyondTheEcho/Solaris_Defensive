using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;

public class BossLoader : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private UnityEvent callback;
    
    public void LoadBoss()
    {
        SceneManager.LoadBossScene(bossPrefab, () => callback.Invoke());
    }
}
