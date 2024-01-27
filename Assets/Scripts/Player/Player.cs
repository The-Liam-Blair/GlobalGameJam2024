using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int id { get; private set; }
    public int health { get; private set; }


    // Control schemes per player, direct string references to the name of the inputs in the input manager.
    public string horizontalInput { get; private set; }
    public string verticalInput { get; private set; }


    // Reference to the player game object.
    public GameObject playerObject { get; private set; }


    // Used by the game manager to assign each set of inputs to each player.
    public bool isAssignedInput { get; set; } = false;

    public Player(GameObject _playerObject, int _id)
    {
        playerObject = _playerObject;
        id = _id;
        health = 100;

        switch (id)
        {
            case 1:
                horizontalInput = "P1Horizontal";
                verticalInput = "P1Vertical";
                break;

            case 2:
                horizontalInput = "P2Horizontal";
                verticalInput = "P2Vertical";
                break;

            default:
                Debug.Log("Player constructor received a id that is out of the expected range.");
                break;
        }
    }
}
