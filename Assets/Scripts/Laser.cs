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
        laserDamage = laserDmg;
    }

    public int ReturnLaserDamage()
    {
        return laserDamage;
    }
}
