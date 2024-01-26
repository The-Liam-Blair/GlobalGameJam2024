using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 MovementInput;

    private Player player;
    private string XInput;
    private string YInput;

    private void Start()
    {
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        player = manager.AssignPlayerToInput();
        XInput = player.horizontalInput;
        YInput = player.verticalInput;
        Debug.Log($"Player {player.id}, {XInput} : {YInput}");
    }

    // Update is called once per frame
    private void Update()
    {
        MovementInput.x = Input.GetAxisRaw(XInput);
        MovementInput.y = Input.GetAxisRaw(YInput);

        transform.Translate(MovementInput * 0.1f);
    }
}
