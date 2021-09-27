using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void calcDamage(GameObject laser, GameObject enemy)
    {
        int dmg = laser.GetComponent<Laser>().ReturnLaserDamage();
        enemy.gameObject.GetComponent<Enemy>().ApplyDamage(dmg);
    }
}
