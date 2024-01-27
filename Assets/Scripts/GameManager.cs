using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;

    private Player player1;
    private Player player2;

    private void Start()
    { 
        player1 = new Player(Instantiate(playerObject, new Vector3(1, 1, 0), Quaternion.identity), 1);
        player2 = new Player(Instantiate(playerObject, new Vector3(5, 1, 0), Quaternion.identity), 2);
    }

    /// <summary>
    /// Dynamically assign the WASD or arrow key control scheme to each player.
    /// </summary>
    /// <returns>Player class reference.</returns>
    public Player AssignInputToPlayer()
    {
        if (!player1.isAssignedInput) {  player1.isAssignedInput = true; return player1; }
        else if (!player2.isAssignedInput) { player2.isAssignedInput = true; return player2; }

        Debug.Log("Over 2 players attempted to be assigned inputs!");
        return null;
    }
}
