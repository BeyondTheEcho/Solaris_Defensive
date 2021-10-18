using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;
    
    //Config
    [Header("Player Variables")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;

    //Player Health Variable
    [Header("Health Variables")]
    [SerializeField] float playerShields = 50;
    [SerializeField] int playerMaxShields = 50;
    [SerializeField] int playerHull = 100;
    [SerializeField] int playerMaxHull = 100;
    [SerializeField] int shieldRegenDelay = 5;
    [SerializeField] int shieldRegenAmount = 5;
    private HullBarScript hullBar;
    private ShieldBarScript shieldBar;

    [Header("Death Variable")]
    [SerializeField] int playerDeathDelay = 1;
    [SerializeField] bool isPlayerDead = false;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip explosionSound;

    [Header("Laser Variables")]
    [SerializeField] float laserFiringPeriod = 0.2f;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] int laserDamage = 100;
    [SerializeField] AudioClip laserSound;
    [SerializeField] GameObject LaserPrefab;

    [Header("FTL Variables")]
    [SerializeField] GameObject ftlParticleSystem;

    // In Script Config / Variables
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    private Coroutine _laserCoroutine;
    private Coroutine shieldRegen;

    private bool _dead;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        EstablishMoveBoundaries();

        hullBar = FindObjectOfType<HullBarScript>();
        shieldBar = FindObjectOfType<ShieldBarScript>();
        
        SetHealthBars();
    }

    // Update is called once per frame   
    void Update()
    {
        PlayerMove();
        FireZeLaserz();
        ActivateDisplacementDrive();
        PlayerDeathCheck();
        UpdateHealth();
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

    private void OnTriggerEnter2D(Collider2D laserHit)
    {
        Laser laser = laserHit.GetComponent<Laser>();

        if (!laser)
        {
            return;
        }
        else
        {
            TakeDamage(laser.ReturnLaserDamage());
        }

        Destroy(laser.gameObject);
        
        if (shieldRegen != null)
        {
            StopCoroutine(shieldRegen);
        }
        
        shieldRegen = StartCoroutine(ShieldRegen());
    }

    IEnumerator ShieldRegen()
    {
        yield return new WaitForSeconds(shieldRegenDelay);
        
        while (playerShields < playerMaxShields)
        {

            playerShields = Mathf.MoveTowards(playerShields, playerMaxShields, shieldRegenAmount);

            yield return new WaitForSeconds(1);
        }
    }

    private void PlayerDeathCheck()
    {
        //Checks to see if the players hull has reached 0 or less than 0
        if (playerHull <= 0 && isPlayerDead == false)
        {
            isPlayerDead = true;

            //Kills the player
            StartCoroutine(playerDeath());          
        }

    }

    //Runs in update, Checks to see if the player is firing their lasers
    private void FireZeLaserz()
    {

        if (Input.GetKeyDown(KeyCode.Space) && _laserCoroutine == null && !_dead)
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
                Quaternion.identity);

            thisLaser.GetComponent<Laser>().SetLaserDamage(laserDamage);

            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, PlayerPrefs.GetFloat("GameVol" , 0.25f));

            //Acts on the Rb component of THIS specific instantiated laser and passes in laserSpeed
            thisLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(laserSpeed, 0);

            //Delays the coroutine by the amount of time in the serialized var laserFiringPeriod
            yield return new WaitForSeconds(laserFiringPeriod);
        }
    }


    IEnumerator playerDeath()
    {
        _dead = true;
        if(_laserCoroutine != null) StopCoroutine(_laserCoroutine);
        
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, PlayerPrefs.GetFloat("GameVol" , 0.25f));

        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(playerDeathDelay);

        SceneManager.LoadScene(SceneManager.Scenes.MainMenu);
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

    private void UpdateHealth()
    {
        hullBar.updateHullBar(playerHull);
        shieldBar.updateShieldBar((int)playerShields);
    }

    private void SetHealthBars()
    {
        hullBar.SetHullBarMax(playerHull);
        shieldBar.SetShieldBarMax((int)playerShields);
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

    private void ActivateDisplacementDrive()
    {
        if (Input.GetKeyDown("g"))
        {
            Instantiate(ftlParticleSystem, transform.position, Quaternion.identity);
        }
    }
}
