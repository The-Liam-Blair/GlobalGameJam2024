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
        MovementScalar = 4000;
    }

    private void FixedUpdate()
    {
        MovementInput.x = Input.GetAxisRaw(XInput);
        MovementInput.y = Input.GetAxisRaw(YInput);

        // Set facing direction to movement direction.
        // Note that a speed of 0 does not change the facing direction, retaining the last direction.
        switch (MovementInput.x)
        {
            case > 0:
                player.isFacingRight = true;
                break;

            case < 0:
                player.isFacingRight = false;
                break;
        }

        player.AttackHitBox.transform.position = player.playerObject.transform.position +
                                                 (player.isFacingRight ? Vector3.right : Vector3.left);

        // Velocity required for good and stable collisions between moving objects.
        // Velocity is set per frame, regardless of input, to halt all acceleration/deceleration after an input from RigidBody movement.
        gameObject.GetComponent<Rigidbody>().AddForce(MovementInput * MovementScalar * Time.fixedDeltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!player.AttackHitBox.GetComponent<PlayerAttackHitBox>().attackActive)
            {
                player.AttackHitBox.GetComponent<PlayerAttackHitBox>().PrepareNextAttack(50, 10, 10, new Vector3(0.33f, 0.33f, 0.33f), 0.5f);
            }
        }
    }
}
