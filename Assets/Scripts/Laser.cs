using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //config
    [SerializeField] AudioClip pew;
    [SerializeField] int laserDamage = 0;

    public void SetLaserDamage(int laserDmg)
    {
        laserDmg = laserDamage;
    }

    public int ReturnLaserDamage()
    {
        return laserDamage;
    }
}
