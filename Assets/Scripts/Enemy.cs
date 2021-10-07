using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroySelf();
    }


    private void OnTriggerEnter2D(Collider2D laser)
    {
        DamageController damageController = laser.GetComponent<DamageController>();
        health -= damageController.ReturnDamage();
        Destroy(laser.gameObject);
    }

    public void DestroySelf()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyDamage(int laserDamage)
    {
        health -= laserDamage;
    }
}
