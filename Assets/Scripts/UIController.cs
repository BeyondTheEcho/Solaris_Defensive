using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    //This Function Starts The Game When Called As An On Click Function
    public void StartGame()
    {
        //Loads The Level 1 Scene
        SceneManager.LoadScene("Level 1");
    }

}
