using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    [SerializeField] int health = 5;

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
        laser.gameObject.GetComponent<Laser>().DestroyLaser();
        FindObjectOfType<DamageController>().calcDamage(laser.gameObject, gameObject);
    }
    public void DestroySelf()
    {
        if (health <= 0)
        {
            Object.Destroy(gameObject);
        }
    }

    public void ApplyDamage(int laserDamage)
    {
        health -= laserDamage;
        Debug.Log(health.ToString()); 
    }
}
