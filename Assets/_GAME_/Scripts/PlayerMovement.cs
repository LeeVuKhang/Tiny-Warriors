﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody2D rb;
    public int facingDirection = 1;
    public Animator anim;

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Debug.Log($"Input: {horizontal}, {vertical}");

        // Flip logic
        if ((horizontal > 0 && transform.localScale.x < 0) ||
            (horizontal < 0 && transform.localScale.x > 0))
        {
            Flip();
        }

        // Animation parameters
        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));

        Vector2 inputVector = new Vector2(horizontal, vertical);

        Vector2 move = inputVector.magnitude > 0.1f ? inputVector.normalized : Vector2.zero;

        rb.linearVelocity = move * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z
        );
    }
}