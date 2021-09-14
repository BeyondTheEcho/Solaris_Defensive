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

    private void Awake()
    {
        rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(0.0f, speed);
        AudioSource.PlayClipAtPoint(pew, transform.position, laserVol);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
