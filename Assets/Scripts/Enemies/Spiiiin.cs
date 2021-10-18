using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiiiin : MonoBehaviour
{
    [SerializeField] float spinSpeed = 1000f;
    
    // Update is called once per frame
    void Update()
    {
        Spin();
    }

    private void Spin() 
    {
	transform.Rotate(0,0, spinSpeed * Time.deltaTime);
    }
}
