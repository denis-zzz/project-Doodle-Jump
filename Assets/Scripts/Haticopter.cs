using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haticopter : MonoBehaviour
{
    public float jumpForce;
    private bool fall = false;

    void FixedUpdate()
    {

        if (transform.parent != null &&
        transform.parent.gameObject.CompareTag("player") &&
        transform.parent.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            fall = true;
            transform.parent.GetComponent<BoxCollider2D>().enabled = true;
        }

        if (fall)
        {
            GetComponent<AudioSource>().Stop();
            transform.parent = null;
            transform.position -= new Vector3(0, 0.1f, 0);
        }

        Vector3 bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        if (transform.position.y < bottom_left.y)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            transform.parent = other.transform;
            if (name == "Haticopter(Clone)")
                transform.localPosition = new Vector3(0, 0.2f, 0);
            else
                transform.localPosition = new Vector3(-0.2f, -0.2f, 0);

            GetComponent<BoxCollider2D>().enabled = false;
            transform.parent.GetComponent<BoxCollider2D>().enabled = false;

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
}
