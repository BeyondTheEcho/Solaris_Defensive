using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //config
    [SerializeField] AudioClip pew;
    [SerializeField] float laserVol = 1.0f;
    [SerializeField] int laserDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = new Vector2(speed, 0f);
        AudioSource.PlayClipAtPoint(pew, Camera.main.transform.position, laserVol);
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

    
    public int ReturnLaserDamage()
    {
        return laserDamage;
    }
}
