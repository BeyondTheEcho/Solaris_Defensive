using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class BossLoader : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    
    public void LoadBoss()
    {
        SceneManager.LoadBossScene(bossPrefab, () => SceneManager.LoadScene(SceneManager.Scenes.Map));
    }
}
