using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MusicController : MonoBehaviour
{
    //Config
    public static MusicController Instance;

    [SerializeField] [Range(0, 1)] private float defaultMusicVol = 0.25f;

    public AudioSource musicPlayer;
    
    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        musicPlayer = gameObject.GetComponent<AudioSource>();
        musicPlayer.volume = defaultMusicVol;
    }
}
