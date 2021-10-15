using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    //Config
    [Header("Menu Config: ")]
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject mainMenu;

    [Header("Dev Menu Config: ")]
    [SerializeField] GameObject devMenuCanvas;
    [SerializeField] GameObject startDevMenu;

    [Header("Enemy & Player: ")]
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject player;

    [Header("Enemy Spawn Position: ")]
    [SerializeField] GameObject enemySpawnPos;
    [SerializeField] GameObject enemySpawnPos2;
    [SerializeField] GameObject enemySpawnPos3;

    [Header("Damage System: ")]
    [SerializeField] int damageDealtToPlayer = 10;

    [Header("Healing System: ")]
    [SerializeField] int giveHealthToPlayer = 10;

    int enemyPosIndex = 0;

    bool menuSwitch = false;
    bool buttonSwitch = true;

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
        SceneManager.LoadScene(SceneManager.Scenes.Map);
    }

    public void ExitGame()
    {
        //Exits Game
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }

    public void OptionsMenuChecker()
    {
            if (menuSwitch == true)
            {
                optionsMenu.gameObject.SetActive(true);
                mainMenu.gameObject.SetActive(false);
            }
            else if (menuSwitch == false)
            {
                optionsMenu.gameObject.SetActive(false);
                mainMenu.gameObject.SetActive(true);
            }
    }
    public void MenuSwitcher()
    {
        if (menuSwitch == true)
        {
            menuSwitch = false;
        }
        else if (menuSwitch == false)
        {
            menuSwitch = true;
        }
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(SceneManager.Scenes.Credits);
    }

    public void LoadDevTest()
    { 
        SceneManager.LoadScene(SceneManager.Scenes.DevPlayground);
    }

    public void SpawnEnemy()
    {

        if (enemyPosIndex == 0)
        {
            Instantiate(enemy, enemySpawnPos.transform.position, Quaternion.identity);
            enemyPosIndex++;
        }
        else if (enemyPosIndex == 1)
        {
            Instantiate(enemy, enemySpawnPos2.transform.position, Quaternion.identity);
            enemyPosIndex++;
        }
        else if (enemyPosIndex == 2)
        {
            Instantiate(enemy, enemySpawnPos3.transform.position, Quaternion.identity);
            enemyPosIndex = 0;
        }

    }

    public void DealDamageToPlayer()
    {
        player.GetComponent<Player>().TakeDamage(damageDealtToPlayer);
    }

    public void HealPlayer()
    {
        player.GetComponent<Player>().GiveHealth(giveHealthToPlayer);
    }

    public void ButtonConfig()
    {
        devMenuCanvas.SetActive(buttonSwitch);
        startDevMenu.SetActive(!buttonSwitch);
        buttonSwitch = !buttonSwitch;
    }
}
