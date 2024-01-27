using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MovementInput;

    private Player player;
    private string XInput;
    private string YInput;

    private float MovementScalar;

    private bool IsMovementDisabled = false;

    private void Start()
    {
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Fetch the input scheme from the game manager.
        player = manager.AssignInputToPlayer();
        XInput = player.horizontalInput;
        YInput = player.verticalInput;

        // Movement speed multiplier of the player.
        MovementScalar = 120;
    }

    private void FixedUpdate()
    {
        MovementInput = Vector2.zero;

        if (!IsMovementDisabled)
        {
            MovementInput.x = Input.GetAxisRaw(XInput);

            // Velocity required for good and stable collisions between moving objects.
            // Velocity is set per frame, regardless of input, to halt all acceleration/deceleration after an input from RigidBody movement.
            gameObject.GetComponent<Rigidbody>().AddForce(MovementInput * MovementScalar);

            MovementInput.y = Input.GetAxisRaw(YInput);
        }

        if (MovementInput is { y: < 0, x: 0 }
            && !player.AttackHitBox.GetComponentInParent<PlayerAttackHitBox>().isPreparingSpinAttack
            && player.AttackHitBox.GetComponent<PlayerAttackHitBox>().duration <= 0f)
        {
            player.AttackHitBox.GetComponent<PlayerAttackHitBox>().PrepareSpinAttack();
        }

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


        if (player.AttackHitBox.GetComponent<PlayerAttackHitBox>().isPerformingSpinAttack)
        {
            float duration = player.AttackHitBox.GetComponent<PlayerAttackHitBox>().duration;
            StartCoroutine(KnockbackVulnerability(duration));
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(duration * (player.isFacingRight ? 5 : -5), 0, 0), ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!player.AttackHitBox.GetComponent<PlayerAttackHitBox>().attackActive)
            {
                player.AttackHitBox.GetComponent<PlayerAttackHitBox>().PrepareNextAttack(50, 10, 10, new Vector3(0.33f, 0.33f, 0.33f), 0.5f);
            }
        }
    }
    public IEnumerator KnockbackVulnerability(float duration = 1f)
    {
        gameObject.GetComponent<Rigidbody>().drag = 5f;
        IsMovementDisabled = true;
        yield return new WaitForSeconds(duration);

        IsMovementDisabled = false;
        gameObject.GetComponent<Rigidbody>().drag = 15f;
        yield return null;
    }
}
