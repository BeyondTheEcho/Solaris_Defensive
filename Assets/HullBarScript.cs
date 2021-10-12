using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HullBarScript : MonoBehaviour
{
    public Slider hullBarSlider;

    public void SetHullBarMax(int maxHull)
    {
        hullBarSlider.maxValue = maxHull;
        hullBarSlider.value = maxHull;
    }

    public void updateHullBar(int hull)
    {
        hullBarSlider.value = hull;
    }
}
