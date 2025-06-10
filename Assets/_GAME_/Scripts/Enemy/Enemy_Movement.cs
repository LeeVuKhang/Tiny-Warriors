using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public int speed = 2;
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
        if (state == EnemyState.Chasing && player != null)
        {
            if ((player.position.x > transform.position.x && facingDirection == -1) ||
                (player.position.x < transform.position.x && facingDirection == 1))
            {
                Flip();
            }

            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.transform;
            ChangeState(EnemyState.Chasing);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
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

        // Update state
        state = newState;

        // Start new animation
        if (state == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        else if (state == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
    }
}

public enum EnemyState
{
    Idle,
    Chasing
}