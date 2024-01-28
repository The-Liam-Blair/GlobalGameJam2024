using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MovementInput;

    public Player player;
    private string XInput;
    private string YInput;
    private string PunchInput;
    private string KickInput;

    private float MovementScalar;

    public bool IsMovementDisabled = false;

    private bool IsTouchingGround = false;
    private float GravityMultiplier;

    private Player otherPlayer;

    public Animator anim;

    public bool isDead = false;

    private GameManager manager;

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Fetch the input scheme from the game manager.
        player = manager.AssignInputToPlayer();

        XInput = player.horizontalInput;
        YInput = player.verticalInput;
        PunchInput = player.PunchInput;
        KickInput = player.KickInput;

        // Movement speed multiplier of the player.
        MovementScalar = 120;
        GravityMultiplier = 0f;

        anim = GetComponent<Animator>();

        if(player.id == 1)
        {
            anim.Play("_P1 Idle");
        }
        else
        {
            anim.Play("_P2 Idle");

        }

        otherPlayer = manager.GetOtherPlayerReference(player.id);
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position + transform.right, Vector3.down, 1.75f) || Physics.Raycast(transform.position - transform.right, Vector3.down, 1.75f))
        {
            IsTouchingGround = true;
            GravityMultiplier = 1f;
        }
        else
        {
            IsTouchingGround = false;
        }
        Debug.DrawRay(transform.position + transform.right, Vector3.down * 1.75f, Color.red);
        Debug.DrawRay(transform.position - transform.right, Vector3.down * 1.75f, Color.blue);

        if (!IsTouchingGround)
        {
            GravityMultiplier += 1.5f;
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 4 * GravityMultiplier, ForceMode.Acceleration);
        }

        MovementInput = Vector2.zero;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -13f, 13f), transform.position.y, transform.position.z);

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

        else if (MovementInput.y > 0 && IsTouchingGround)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 100, ForceMode.Impulse);
            FindObjectOfType<AudioManager>().PlayJumpSound();

        }

        if (MovementInput.x != 0 && IsTouchingGround)
        {
            FindObjectOfType<AudioManager>().PlayerFootSteps();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            FindObjectOfType<AudioManager>().Play("PlayerPunchBuildUp");
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            FindObjectOfType<AudioManager>().Stop("PlayerPunchBuildUp");
        }

        // Set facing direction to movement direction.
            // Note that a speed of 0 does not change the facing direction, retaining the last direction.
            switch (MovementInput.x)
        {
            case > 0:

                player.isFacingRight = true;
                player.playerObject.transform.rotation = Quaternion.Euler(0, player.id == 1 ? 0 : 180, 0);
                break;

            case < 0:
                player.isFacingRight = false;
                player.playerObject.transform.rotation = Quaternion.Euler(0, player.id == 1 ? 180 : 0,  0);

                break;
        }

        player.AttackHitBox.transform.position = player.playerObject.transform.position +
                                                 (player.isFacingRight ? Vector3.right : Vector3.left);


        if (player.AttackHitBox.GetComponent<PlayerAttackHitBox>().isPerformingSpinAttack)
        {
            float duration = player.AttackHitBox.GetComponent<PlayerAttackHitBox>().duration;
            StartCoroutine(KnockbackVulnerability(duration));
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(duration * (player.isFacingRight ? 4 : -4), 0, 0), ForceMode.Impulse);
        }

        if (otherPlayer.health <= 0)
        {
            if (player.id == 1)
            {
                anim.Play("_P1 Laugh");
            }
            else
            {
                anim.Play("_P2 Laugh");
            }
            GetComponent<Rigidbody>().detectCollisions = false;
            IsMovementDisabled = true;
            otherPlayer.playerObject.GetComponent<PlayerInput>().IsMovementDisabled = true;
        }

        if (Input.GetAxisRaw(PunchInput) > 0 && !player.AttackHitBox.GetComponent<PlayerAttackHitBox>().attackActive)
        {
            if (!player.AttackHitBox.GetComponent<PlayerAttackHitBox>().attackActive)
            {
                if (player.id == 1)
                {
                    anim.Play("_P1 Punch");
                }

                else
                {
                    anim.Play("_P2 Punch");
                }
                player.AttackHitBox.GetComponent<PlayerAttackHitBox>().PreparePunchAttack();
                FindObjectOfType<AudioManager>().PlayerWhoosh();

            }
        }
        else if (Input.GetAxisRaw(KickInput) > 0 &&
                 !player.AttackHitBox.GetComponent<PlayerAttackHitBox>().attackActive)
        {
            if (!player.AttackHitBox.GetComponent<PlayerAttackHitBox>().attackActive)
            {
                if (player.id == 1)
                {
                    anim.Play("_P1 Kick");
                }

                else
                {
                    anim.Play("_P2 Kick");
                }

                player.AttackHitBox.GetComponent<PlayerAttackHitBox>().PrepareKickAttack();
                FindObjectOfType<AudioManager>().PlayerWhoosh();
            }
        }
    }

    public void TakeDamage(int incDamage)
    {
        player.TakeDamage(incDamage);
    }

    public void Die()
    {
        StartCoroutine(DeathAnimation());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Vector3 oppositeVel = gameObject.GetComponent<Rigidbody>().velocity;
            oppositeVel.x *= -1;
            oppositeVel.y *= -1;

            gameObject.GetComponent<Rigidbody>().AddForce(oppositeVel * 2, ForceMode.Impulse);
        }
    }
    
    public IEnumerator KnockbackVulnerability(float duration = 0.75f)
    {
        gameObject.GetComponent<Rigidbody>().drag = 5f;
        IsMovementDisabled = true; 
        yield return new WaitForSeconds(duration);

        IsMovementDisabled = false;
        gameObject.GetComponent<Rigidbody>().drag = 15f;
        yield return null;
    }

    private IEnumerator DeathAnimation()
    {
        yield return new WaitForSecondsRealtime(0.33f);
        if (player.id == 1)
        {
            anim.Play("_P1 KO");
        }
        else
        {
            anim.Play("_P2 KO");
        }
        IsMovementDisabled = true;
        isDead = true;
        GetComponent<BoxCollider>().size = new Vector3(2.55f, 1.75f, 1f);

        yield return new WaitForSecondsRealtime(1f);
        yield return null;
    }
}