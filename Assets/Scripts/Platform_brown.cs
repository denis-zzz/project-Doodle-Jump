using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_brown : MonoBehaviour
{
    public Animator animator;
    private bool fall = false;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("player") && other.relativeVelocity.y <= 0f)
        {
            GetComponent<EdgeCollider2D>().enabled = false;
            GetComponent<PlatformEffector2D>().enabled = false;
            animator.SetBool("break", true);
            GetComponent<AudioSource>().Play();
            fall = true;
        }
    }

    void FixedUpdate()
    {
        if (fall)
            transform.position -= new Vector3(0, 0.01f, 0);

        Vector3 bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        if (transform.position.y < bottom_left.y)
            Destroy(gameObject);
    }
}
