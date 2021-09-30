using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public Slider slider;
    public Slider slider2;

    // Start is called before the first frame update
    void Start()
    {
        SetHull(100);
        SetShields(50);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMaxShields(int shields)
    { 
        slider.maxValue = shields;
        slider.value = shields;
    }
    public void SetShields(int shields)
    {
        slider.value = shields;
    }

    public void SetMaxHull(int hull)
    {
        slider2.maxValue = hull;
        slider2.value = hull;
    }

    public void SetHull(int hull)
    {
        slider2.value = hull;
    }

    public void ShieldChecker()
    {

    }
}


