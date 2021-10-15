using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float jumpForce = 15f;
    public Animator animator;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("player") && other.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 vel = rb.velocity;
                vel.y = jumpForce;
                rb.velocity = vel;

                GetComponent<AudioSource>().Play();
                animator.SetBool("activate", true);
            }
        }
    }
}
