using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Config
    [SerializeField] GameObject player;
    [SerializeField] Sprite ship1;
    [SerializeField] Sprite ship2;
    [SerializeField] Sprite ship3;
    [SerializeField] Sprite ship4;
    [SerializeField] Sprite ship5;
    int currentShip = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Z"))
        {
            ChangeShipSprite();              
        }
    }

    public void ChangeShipSprite()
    {
        Sprite[] shipSprites = new Sprite[] { ship1, ship2, ship3, ship4, ship5 };

        if (currentShip >= shipSprites.Length - 1)
        {
            currentShip = 0;
        }
        else
        {
            currentShip++;
        }

        player.GetComponent<SpriteRenderer>().sprite = shipSprites[currentShip];
    }
}
