using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Config
    [Header("Player Variables")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] float laserFiringPeriod = 0.2f;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] int playerDeathDelay = 5;

    //Player Health Variable
    [Header("Player Variables")]
    [SerializeField] int playerShields = 50;
    [SerializeField] int playerHull = 100;

    // In Script Config / Variables
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    public GameObject LaserPrefab;

    private Coroutine _laserCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        EstablishMoveBoundaries();
        StartCoroutine(PlayerDeathCheck());
    }

    // Update is called once per frame   
    void Update()
    {
        PlayerMove();
        FireZeLaserz();
    }

    private void PlayerMove()
    {
        // Gets Input from Axis in "Unity Project Seetings > Input > Axes"
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        // Calculates the new X and Y Positions by adding the change(Δ : Delta) of X and Y
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        // Acts upon the Player objects transform component
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void EstablishMoveBoundaries()
    {
        // Assigns the variable gameCamera, of type Camera, to the Main Camera object
        Camera gameCamera = Camera.main;

        // Establishes the X min and max values as they relate to the gamespace, relative to the Cameras boundaries
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        // Establishes the Y min and max values as they relate to the gamespace, relative to the Cameras boundaries
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    //A coroutine that is called in start to check if the player has died
    IEnumerator PlayerDeathCheck()
    {
        //Checks to see if the players hull has reached 0 or less than 0
        if(playerHull <= 0)
        {
            //waits for the player death delay
            yield return new WaitForSeconds(playerDeathDelay);

            //Kills the player
            playerDeath();
        }

    }

    //Runs in update, Checks to see if the player is firing their lasers
    private void FireZeLaserz()
    
    {
  
        if (Input.GetKeyDown(KeyCode.Space) && _laserCoroutine == null)
        {
            //When the space button is pressed the FireWhileHeld() coroutines starts and is assigned to fireConstantly var
            _laserCoroutine = StartCoroutine(FireWhileHeld());
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            //When the space bar is released, stops the coroutine that fires constantly
            StopCoroutine(_laserCoroutine);
            _laserCoroutine = null;
        }
    }

    //Coroutine that allows the player to fire while the space button is held
    IEnumerator FireWhileHeld()
    {
        //Always runs
        while (true)
        {
            //Instantiates the laser prefab just ahead of the player object
            var thisLaser = Instantiate(LaserPrefab, 
                gameObject.transform.position + Vector3.right * 1, 
                Quaternion.Euler(new Vector3(0,0,90)));

            //Acts on the Rb component of THIS specific instantiated laser and passes in laserSpeed
            thisLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(laserSpeed, 0);

            //Delays the coroutine by the amount of time in the serialized var laserFiringPeriod
            yield return new WaitForSeconds(laserFiringPeriod);
        }         
    }

    //A currently unused class that will load the Main Menu on player death
    private void playerDeath()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void TakeDamage(int dmg)
    {
        if (playerShields > 0)
        {
            playerShields -= dmg;
        }
        else if (playerShields <= 0)
        {
            playerHull -= dmg;
        }
        Debug.Log("playerShields: " + playerShields);
        Debug.Log("playerHull: " + playerHull);
    }

    public void GiveHealth(int hp)
    {
        if (playerShields < 50)
        {
            playerShields += hp;
        }
        else if (playerHull < 100)
        {
            playerHull += hp;
        }
        

        if (playerShields > 50)
        {
            playerShields = 50;
        }
        else if (playerHull > 100)
        {
            playerHull = 100;
        }

        Debug.Log("playerShields: " + playerShields);
        Debug.Log("playerHull: " + playerHull);
    }
}
