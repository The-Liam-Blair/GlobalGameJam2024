using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttackHitBox : MonoBehaviour
{
    // Attack and animation is still playing
    public bool attackActive;

    // Attack damage and knockback force on the X and Y axes.
    private int attackDamage;
    private float attackHorizontalForce;
    private float attackVerticalForce;
    
    // Size of the hitbox generated by the attack.
    private Vector2 attackHitBoxSize;

    // Time in seconds to fully perform the attack.
    public float duration;

    private BoxCollider hitBox;
    private PlayerInput playerInput;

    public bool isPreparingSpinAttack = false;
    private float ChargeDuration;
    public bool isPerformingSpinAttack = false;
    private float SpinDuration;

    private bool chargeAniPlaying = false;

    private bool chargeAtkSound = true;

    private void Start()
    {
        hitBox = gameObject.GetComponent<BoxCollider>();

        attackDamage = 0;

        attackHorizontalForce = 0;
        attackVerticalForce = 0;

        duration = 0;

        attackActive = false;
        hitBox.size = Vector2.zero;
        gameObject.transform.localScale = Vector3.zero;
        hitBox.enabled = false;

        playerInput = transform.parent.GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (attackActive)
        {

            duration -= Time.deltaTime;
            if (duration <= 0f)
            {
                attackActive = false;
                DeactivateAttack();
            }
        }

        if (isPreparingSpinAttack)
        {
            if (playerInput.MovementInput is { x: 0f, y: < 0f })
            {
                ChargeDuration += Time.deltaTime;
                
                if (!chargeAniPlaying)
                {
                    if (playerInput.player.id == 1)
                    {
                        playerInput.anim.Play("_P1 Sprint");
                    }
                    else
                    {
                        playerInput.anim.Play("_P2 Sprint");
                    }
                    chargeAniPlaying = true;
                }
            }
            else if (playerInput.MovementInput.x != 0f || playerInput.MovementInput.y >= 0f)
            {
                Debug.Log($"Stopped spinning at {ChargeDuration}");
                isPreparingSpinAttack = false;
            }
        }

        if (!isPreparingSpinAttack && ChargeDuration > 0.1f)
        {
            switch (ChargeDuration)
            {
                case > 0.66f:
                    isPerformingSpinAttack = true;
                    float duration = ChargeDuration * 0.5f;
                    Mathf.Clamp(duration, 2f, 4f);

                    int damage = (int) Mathf.Floor(ChargeDuration * 8);
                    Mathf.Clamp(damage, 1, 40);

                    float Hforce = ChargeDuration * 60;
                    float Vforce = ChargeDuration * 30;

                    PrepareNextAttack(damage, Hforce, Vforce, new Vector3(1.2f, 1f, 1f), duration, "");
                    Debug.Log($"Spin attack! Duration: {duration}, Damage: {damage}, Hforce: {Hforce}, Vforce: {Vforce}");
                    break;

                default:
                    if (playerInput.player.id == 1)
                    {
                        playerInput.anim.Play("_P1 Idle");
                    }
                    else
                    {
                        playerInput.anim.Play("_P2 Idle");
                    }

                    break;
            } 
            chargeAniPlaying = false;
            ChargeDuration = 0;
        }
    }
    
    public void PrepareNextAttack(int _damage, float _Hforce, float _Vforce, Vector3 _size, float _duration, string type)
    {
        attackDamage = _damage;
        
        attackHorizontalForce = _Hforce;
        attackVerticalForce = _Vforce;

        duration = _duration;

        attackActive = true;
        attackHitBoxSize = _size;

        hitBox.enabled = true;
        hitBox.size = attackHitBoxSize;

        gameObject.transform.localScale = _size;
    }

    public void PrepareSpinAttack()
    {
        Debug.Log("Starting spin...");
        isPreparingSpinAttack = true;
    }

    public void PreparePunchAttack()
    {
        PrepareNextAttack(8, 30, 20, new Vector3(1.2f, 0.5f, 1f), 1f, "Punch");
    }

    public void PrepareKickAttack()
    {
        PrepareNextAttack(10, 20, 35, new Vector3(0.8f, 1.25f, 1f), 0.75f, "Kick");
    }

    private void DeactivateAttack()
    {
        attackDamage = 0;
        
        attackHorizontalForce = 0;
        attackVerticalForce = 0;
        
        duration = 0;
        
        attackActive = false;
        hitBox.size = Vector2.zero;
        hitBox.enabled = false;

        if (isPerformingSpinAttack)
        {
            isPerformingSpinAttack = false;
        }

        if (playerInput.player.id == 1)
        {
            playerInput.anim.Play("_P1 Idle");
        }
        else
        {
            playerInput.anim.Play("_P2 Idle");
        }

        gameObject.transform.localScale = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform != transform.parent && other.transform.CompareTag("Player"))
        {
            // Direction is from the parent of the hitbox (which is the attacking player) to the other player.
            Vector3 normalizedDirection = Vector3.Normalize(other.transform.position - transform.parent.position);

            // Normalization done to get the direction of the knockback force, the y axis is added after as its likely the factor
            // for the y axis is an extremely small number from a tiny y value difference.
            Vector2 knockbackForce = new Vector2(attackHorizontalForce, 0) * normalizedDirection;
            knockbackForce.y = attackVerticalForce;

            other.gameObject.GetComponent<PlayerInput>().TakeDamage(attackDamage);
            float rand = Random.Range(0f, 1f);

            switch (rand)
            {
                case <= 0.33f:
                    if (playerInput.player.id == 1)
                    {
                        other.gameObject.GetComponent<PlayerInput>().anim.Play("_P2 Launch");
                    }
                    else
                    {
                        other.gameObject.GetComponent<PlayerInput>().anim.Play("_P1 Launch");
                    }
                    break;

                case <= 0.66f:
                    if (playerInput.player.id == 1)
                    {
                        other.gameObject.GetComponent<PlayerInput>().anim.Play("_P2 Hurt1");
                    }
                    else
                    {
                        other.gameObject.GetComponent<PlayerInput>().anim.Play("_P1 Hurt1");
                    }
                    break;

                case <= 1f:
                    if (playerInput.player.id == 1)
                    {
                        other.gameObject.GetComponent<PlayerInput>().anim.Play("_P2 Hurt2");
                    }
                    else
                    {
                        other.gameObject.GetComponent<PlayerInput>().anim.Play("_P1 Hurt2");
                    }
                    break;
            }

            other.gameObject.GetComponent<Rigidbody>().AddForce(knockbackForce, ForceMode.Impulse);
            
            StartCoroutine(other.gameObject.GetComponent<PlayerInput>().KnockbackVulnerability());
            Debug.Log("hit");
            FindObjectOfType<AudioManager>().PlayerHit();

            DeactivateAttack();
            duration = 0f;
        }
    }
}