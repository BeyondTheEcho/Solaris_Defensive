using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplacementDrive : MonoBehaviour
{
    ParticleSystem displacementEffect;
    ParticleSystem.ShapeModule shape;

    GameObject player;

    private void Awake()
    {
        displacementEffect = GetComponent<ParticleSystem>();
        shape = displacementEffect.shape;
        shape.radius = 3f;

        player = FindObjectOfType<Player>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplacementDriveEffect());
    }

    // Update is called once per frame
    void Update()
    {
        LockToPlayerPosition();
    }

    private void LockToPlayerPosition()
    {
        transform.position = player.transform.position;
    }

    IEnumerator DisplacementDriveEffect()
    {
        yield return new WaitForSeconds(1);

        shape.radius = 2.5f;

        yield return new WaitForSeconds(1);

        shape.radius = 2.0f;

        yield return new WaitForSeconds(1);

        shape.radius = 1.5f;

        yield return new WaitForSeconds(1);

        shape.radius = 1f;

        yield return new WaitForSeconds(1);

        shape.radius = 0.5f;

        Destroy(gameObject);

        yield break;
    }
}
