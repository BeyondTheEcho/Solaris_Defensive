using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBarScript : MonoBehaviour
{
    public Slider shieldBarSlider;

    public void SetShieldBarMax(int maxShield)
    {
        shieldBarSlider.maxValue = maxShield;
        shieldBarSlider.value = maxShield;
    }

    public void updateShieldBar(int shield)
    {
        shieldBarSlider.value = shield;
    }
}
