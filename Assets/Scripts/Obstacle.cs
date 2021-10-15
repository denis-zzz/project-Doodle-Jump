using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameManager gm;
    public int jumpForce;
    private ParticleSystem particles;

    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        particles = GetComponent<ParticleSystem>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        GetComponent<AudioSource>().Play();
        // si le joueur arrive par le dessus
        if (other.gameObject.CompareTag("player") && other.relativeVelocity.y <= 0f
        && name != "Black_hole(Clone)")
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 vel = rb.velocity;
                vel.y = jumpForce;
                rb.velocity = vel;
            }
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        // si par le dessous
        else if (other.gameObject.CompareTag("player"))
        {
            gm.gameOver();
        }
        // sinon c'est une bulle
        else if (name != "Black_hole(Clone)")
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            particles.Play();
        }
    }

    void FixedUpdate()
    {
        Vector3 bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        if (transform.position.y < bottom_left.y)
            Destroy(gameObject);
    }

}
