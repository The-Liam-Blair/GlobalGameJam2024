using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 MovementInput;

    private Player player;
    private string XInput;
    private string YInput;

    private float MovementScalar;

    private void Start()
    {
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Fetch the input scheme from the game manager.
        player = manager.AssignInputToPlayer();
        XInput = player.horizontalInput;
        YInput = player.verticalInput;

        // Movement speed multiplier of the player.
        MovementScalar = 400;
    }

    private void FixedUpdate()
    {
        MovementInput.x = Input.GetAxisRaw(XInput);
        MovementInput.y = Input.GetAxisRaw(YInput);

        // Velocity required for good and stable collisions between moving objects.
        // Velocity is set per frame, regardless of input, to halt all acceleration/deceleration after an input from RigidBody movement.
        gameObject.GetComponent<Rigidbody>().velocity = MovementInput * MovementScalar * Time.fixedDeltaTime;
    }
}
