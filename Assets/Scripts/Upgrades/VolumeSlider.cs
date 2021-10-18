using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public void SetGameVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("GameVol", slider.value);
    }
    
    public void SetMusicVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("MusicVol", slider.value);
        MusicController.Instance.musicPlayer.volume = slider.value;
    }
}
