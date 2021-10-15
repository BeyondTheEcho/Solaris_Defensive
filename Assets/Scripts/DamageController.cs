using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] int damage = 100;

    public int ReturnDamage()
    {
        return damage;
    }
}
