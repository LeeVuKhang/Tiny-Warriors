using System.Collections;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public float speed = 2;
    public float attackRange = 2;
    public float attackCooldown = 2;
    public float attackCooldownTimer;
    public float playerDetectRange = 5;
    public Transform detectionPoint;
    public LayerMask playerLayer;
    public Transform player;
    public EnemyState state;


    private Rigidbody2D rb;
    private int facingDirection = 1;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        if (state != EnemyState.Knockback)
        {
            CheckForPlayer();
            if (attackCooldownTimer > 0)
            {
                attackCooldownTimer -= Time.deltaTime;
            }
            if (state == EnemyState.Chasing)
            {
                Chase();
            }
            else if (state == EnemyState.Attacking)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    private void CheckForPlayer() 
    {
        Collider2D[] inRange = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);
        if (inRange.Length > 0) {
            player = inRange[0].transform;
            //In attackRange then change state to attack
            if (Mathf.Abs(Vector2.Distance(transform.position, player.position)) <= attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            //else > attack range
            else if (Mathf.Abs(Vector2.Distance(transform.position, player.position)) > attackRange && state != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    private void Chase()
    {
        if ((player.position.x > transform.position.x && facingDirection == -1) ||
                (player.position.x < transform.position.x && facingDirection == 1))
        {
            Flip();
        }

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    } 
    private void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z
        );
    }

    public void ChangeState(EnemyState newState)
    {
        // Stop current animation
        if (state == EnemyState.Idle)
            anim.SetBool("isIdle", false);
        else if (state == EnemyState.Chasing)
            anim.SetBool("isChasing", false);
        else if (state == EnemyState.Attacking)
            anim.SetBool("isAttacking", false);

        // Update state
        state = newState;

        // Start new animation
        if (state == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        else if (state == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
        else if (state == EnemyState.Attacking)
            anim.SetBool("isAttacking", true);
    }

    public void Knockback(Transform enemy, float force, float stunTime)
    {
        ChangeState(EnemyState.Knockback);
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.linearVelocity = direction * force;
        StartCoroutine(KnockbackCounter(stunTime));
    }
    IEnumerator KnockbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.linearVelocity = Vector2.zero;
        ChangeState(EnemyState.Idle);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
    }

}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Knockback
}