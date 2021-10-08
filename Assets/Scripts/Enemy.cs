using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] float fireSpeed = 10f;
    [SerializeField] float shotCounter;
    [SerializeField] float minFireDelay = 0.2f;
    [SerializeField] float maxFireDelay = 3f;
    [SerializeField] GameObject enemyLaserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minFireDelay, maxFireDelay);
    }

    // Update is called once per frame
    void Update()
    {
        FireManager();
        DestroySelf();
    }

    private void OnTriggerEnter2D(Collider2D laser)
    {
        DamageController damageController = laser.GetComponent<DamageController>();

        if (!damageController)
        {
            return;
        }

        health -= damageController.ReturnDamage();

        Destroy(laser.gameObject);
    }

    private void FireManager()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minFireDelay, maxFireDelay);
        }
    }

    private void Fire()
    {
        var thisLaser = Instantiate(
            enemyLaserPrefab, 
            transform.position + Vector3.left * 2, 
            Quaternion.identity);

        thisLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(-fireSpeed, 0);
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
