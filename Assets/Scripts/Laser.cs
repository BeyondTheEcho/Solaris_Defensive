using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //config
    [SerializeField] float speed = 2.0f;
    [SerializeField] AudioClip pew;
    [SerializeField] float laserVol = 1.0f;
    private Rigidbody2D rb;

    // Examples - not doing anything
    public float Damage => GameController.Instance.Upgrades.LaserDamage.Value;
    public void UpgradeLaserDamage() => GameController.Instance.Upgrades.LaserDamage.ActiveIndex++;

    private void Awake()
    {
        rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(speed, 0f);
        AudioSource.PlayClipAtPoint(pew, Camera.main.transform.position, laserVol);
    }

    /*void OnCollisionEnter2D(Collider2D other)
    {
        other.GetComponent<SomeEnemyComponent>().TakeDamage(Damage);
    }*/
    
    // Update is called once per frame
    void Update()
    {
        
    }

}
