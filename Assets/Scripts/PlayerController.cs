using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movement_speed = 8;
    public Animator animator;
    public GameObject bubble;

    private float moveX;
    private Vector3 orig_localScale;
    private Rigidbody2D rb;
    float offset = 0.5f;
    private GameManager gm;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        orig_localScale = transform.localScale;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            moveX = Input.GetAxis("Horizontal") * movement_speed;

            // direction du regard du doodler
            if (moveX > 0)
                transform.localScale = new Vector3(orig_localScale.x,
                orig_localScale.y, orig_localScale.z);
            else if (moveX < 0)
                transform.localScale = new Vector3(-orig_localScale.x,
                orig_localScale.y, orig_localScale.z);

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(bubble, transform.position, Quaternion.identity);
                animator.SetBool("shoot", true);
                GetComponent<AudioSource>().Play();
            }
            else animator.SetBool("shoot", false);
        }
    }

    void FixedUpdate()
    {
        Vector2 vel = rb.velocity;
        vel.x = moveX;
        rb.velocity = vel;
        animator.SetFloat("y speed", vel.y);

        // sortie d'écran par la gauche ou la droite
        Vector3 bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        if (transform.position.x > -bottom_left.x + offset)
            transform.position = new Vector3(bottom_left.x - offset,
            transform.position.y, transform.position.z);

        else if (transform.position.x < bottom_left.x - offset)
            transform.position = new Vector3(-bottom_left.x + offset,
            transform.position.y, transform.position.z);
    }
}
