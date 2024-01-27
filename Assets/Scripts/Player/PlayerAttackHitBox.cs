using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttackHitBox : MonoBehaviour
{
    // Attack and animation is still playing
    public bool attackActive;

    // Attack damage and knockback force on the X and Y axes.
    private float attackDamage;
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

    private void Start()
    {
        hitBox = gameObject.GetComponent<BoxCollider>();
        
        // Deactivate attack also can serve to initialize all the variables to standard values.
        DeactivateAttack();

        playerInput = transform.parent.GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrepareNextAttack(50, 10, 10, new Vector3(0.33f, 0.33f, 0.33f), 0.5f);
        }

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
                Debug.Log($"Spinning... {ChargeDuration}");
                ChargeDuration += Time.deltaTime;
            }
            else if (playerInput.MovementInput.x != 0f || playerInput.MovementInput.y >= 0f)
            {
                Debug.Log($"Stopped spinning at {ChargeDuration}");
                isPreparingSpinAttack = false;
            }
        }

        if (!isPreparingSpinAttack)
        {
            switch (ChargeDuration)
            {
                case > 0.66f:
                    isPerformingSpinAttack = true;
                    float duration = ChargeDuration * 0.5f;
                    Mathf.Clamp(duration, 2f, 5f);

                    float damage = ChargeDuration * 4;
                    Mathf.Clamp(damage, 1, 30);

                    float Hforce = ChargeDuration * 10;
                    float Vforce = ChargeDuration * 2;

                    PrepareNextAttack(damage, Hforce, Vforce, new Vector3(1.2f, 1f, 1f), duration);
                    Debug.Log($"Spin attack! Duration: {duration}, Damage: {damage}, Hforce: {Hforce}, Vforce: {Vforce}");
                    break;
            }

            ChargeDuration = 0;
        }
    }
    
    public void PrepareNextAttack(float _damage, float _Hforce, float _Vforce, Vector3 _size, float _duration)
    {
        attackDamage = _damage;
        
        attackHorizontalForce = _Hforce;
        attackVerticalForce = _Vforce;

        duration = _duration;

        attackActive = true;
        attackHitBoxSize = _size;

        hitBox.enabled = true;
        hitBox.size = attackHitBoxSize;

        // TEMPORARY TO VISUALIZE HITBOX SIZES - Remove later!
        gameObject.transform.localScale = _size;
    }

    public void PrepareSpinAttack()
    {
        Debug.Log("Starting spin...");
        isPreparingSpinAttack = true;
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

        // TEMPORARY TO VISUALIZE HITBOX SIZES - Remove later!
        gameObject.transform.localScale = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Current: {gameObject.name}. Other: {other.gameObject.name}.");

        if (other.transform != transform.parent && other.transform.CompareTag("Player"))
        {
            DeactivateAttack();

            // Direction is from the parent of the hitbox (which is the attacking player) to the other player.
            Vector3 normalizedDirection = Vector3.Normalize(other.transform.position - transform.parent.position);

            Debug.DrawRay(transform.position, normalizedDirection * 5f, Color.red, 5f);

            Vector2 knockbackForce = new Vector2(attackHorizontalForce, attackVerticalForce);
            // Other.TakeDamage();

            other.gameObject.GetComponent<Rigidbody>().AddForce(knockbackForce, ForceMode.Impulse);
            StartCoroutine(other.gameObject.GetComponent<PlayerInput>().KnockbackVulnerability());
        }
    }
}
