using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Health Variables")]
    [SerializeField] protected int health = 100;

    [Header("Enemy Weapon Variables")]
    [SerializeField] float fireSpeed = 10f;
    [SerializeField] float minFireDelay = 0.2f;
    [SerializeField] float maxFireDelay = 3f;
    [SerializeField] int laserDamage = 10;
    [SerializeField] [Range(0, 1)] float laserVol = 0.5f;
    [SerializeField] AudioClip laserSound;
    [SerializeField] GameObject enemyLaserPrefab;

    [Header("Enemy Death Variables")]
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float explosionVol = 0.5f;
    [SerializeField] int explosionDelay = 1;


    float shotCounter;

    // Start is called before the first frame update
    protected void Start()
    {
        shotCounter = Random.Range(minFireDelay, maxFireDelay);
    }

    // Update is called once per frame
    protected void Update()
    {
        FireManager();
    }

    private void OnTriggerEnter2D(Collider2D laserHit)
    {
        Laser laser = laserHit.GetComponent<Laser>();

        if (!laser)
        {
            return;
        }

        ApplyDamage(laser.ReturnLaserDamage());

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

        thisLaser.GetComponent<Laser>().SetLaserDamage(laserDamage);

        AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserVol);

        thisLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(-fireSpeed, 0);
    }

    public void ApplyDamage(int laserDamage)
    {
        health -= laserDamage;
        
        if (health <= 0)
        {
            Destroy(gameObject);
            StartCoroutine(TriggerExplosion());
        }
    }

    IEnumerator TriggerExplosion()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, explosionVol);

        yield return new WaitForSeconds(explosionDelay);

        Destroy(explosion);
    }
    
    protected bool _trySubtract(ref float val, float operand, float min = 0)
    {
        if (val - operand < min) return false;
        val -= operand;
        return true;
    }
}
