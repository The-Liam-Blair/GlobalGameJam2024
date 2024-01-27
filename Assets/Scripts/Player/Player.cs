using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class Player
{
    public int id { get; }
    public int health { get; private set; }


    // Control schemes per player, direct string references to the name of the inputs in the input manager.
    public string horizontalInput { get; private set; }
    public string verticalInput { get; private set; }


    // Reference to the player game object.
    public GameObject playerObject { get; private set; }

    public GameObject AttackHitBox { get; private set; }


    // Used by the game manager to assign each set of inputs to each player.
    public bool isAssignedInput { get; set; } = false;
    
    // Used to position the hitbox of attacks.
    public bool isFacingRight { get; set; } = true;

    public Player(GameObject _playerObject, int _id)
    {
        playerObject = _playerObject;
        id = _id;
        health = 100;

        AttackHitBox = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/AttackHitBox"),
            playerObject.transform.position + (isFacingRight ? Vector3.right : Vector3.left),
            Quaternion.identity);

        AttackHitBox.transform.parent = playerObject.transform;

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

    public void TakeDamage(int incDamage)
    {
        health -= incDamage;

        if (health <= 0)
        {
            // die?
        }
    }
}
