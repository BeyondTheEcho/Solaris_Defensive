using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    //Config
    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject MainMenu;

    bool MenuSwitch = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        OptionsMenuChecker();
    }

    //This Function Starts The Game When Called As An On Click Function
    public void StartGame()
    {
        //Loads The Level 1 Scene
        SceneManager.LoadScene(SceneManager.Scenes.Level1);
    }

    public void ExitGame()
    {
        //Exits Game
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void OptionsMenuChecker()
    {
            if (MenuSwitch == true)
            {
                OptionsMenu.gameObject.SetActive(true);
                MainMenu.gameObject.SetActive(false);
            }
            else if (MenuSwitch == false)
            {
                OptionsMenu.gameObject.SetActive(false);
                MainMenu.gameObject.SetActive(true);
            }
    }
    public void MenuSwitcher()
    {
        if (MenuSwitch == true)
        {
            MenuSwitch = false;
        }
        else if (MenuSwitch == false)
        {
            MenuSwitch = true;
        }
    }
    public void LoadCredits()
    {
        SceneManager.LoadScene(SceneManager.Scenes.Credits);
    }
}
