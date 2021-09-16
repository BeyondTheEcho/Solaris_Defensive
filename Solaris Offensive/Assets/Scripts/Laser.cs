using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //Config
    [SerializeField] float speed = 2.0f;
    [SerializeField] AudioClip pew;
    [SerializeField] float laserVol = 1.0f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FireLaser();
    }

    private void FireLaser()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            rb.velocity = new Vector2(0.0f, speed);
            //Plays laser audio clip
            AudioSource.PlayClipAtPoint(pew, transform.position, laserVol);
        }
    }
}
