using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTest : MonoBehaviour
{
    public GameObject Boss;
    void Start()
    {
        SceneManager.LoadBossScene(Boss, () => UnityEngine.SceneManagement.SceneManager.LoadScene("BossTest"));   
    }
}
