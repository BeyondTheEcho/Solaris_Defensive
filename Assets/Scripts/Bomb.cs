using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip explosionClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            TriggerBombEffects();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void TriggerBombEffects()
    {
        AudioSource.PlayClipAtPoint(explosionClip, transform.position, PlayerPrefs.GetFloat("GameVol", 0.25f));
        

        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        

        Destroy(explosion, 1);
    }
}