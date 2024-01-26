using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int id { get; private set; }
    public int health { get; private set; }

    public string horizontalInput { get; private set; }
    public string verticalInput { get; private set; }

    public GameObject gameObject { get; set; }

    public bool isAssignedInput { get; set; } = false;

    public Player(GameObject obj, int _id)
    {
        gameObject = obj;
        id = _id;
        health = 100;

        if (id == 1)
        {
            horizontalInput = "P1Horizontal";
            verticalInput = "P1Vertical";
        }
        else if (id == 2)
        {
            horizontalInput = "P2Horizontal";
            verticalInput = "P2Vertical";
        }

        else
        {
            Debug.Log("Player constructor received a id that is out of the expected range.");
        }
    }
}
