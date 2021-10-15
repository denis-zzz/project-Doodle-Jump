using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_blue : MonoBehaviour
{
    private bool to_right = true;
    float offset = 0.5f;
    public float jumpForce = 10f;

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
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        if (to_right)
        {
            if (transform.position.x < -bottom_left.x - offset)
                transform.position += new Vector3(0.05f, 0, 0);
            else
                to_right = false;
        }
        else
        {
            if (transform.position.x > bottom_left.x + offset)
                transform.position -= new Vector3(0.05f, 0, 0);
            else
                to_right = true;
        }

        if (transform.position.y < bottom_left.y)
            Destroy(gameObject);

    }
}
