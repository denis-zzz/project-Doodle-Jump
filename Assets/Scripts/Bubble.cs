using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    void FixedUpdate()
    {
        Vector3 top_left = Camera.main.ScreenToWorldPoint(
            new Vector3(0, Camera.main.pixelHeight, 0));

        transform.position += new Vector3(0, 0.5f, 0);

        if (transform.position.y >= top_left.y)
            Destroy(gameObject);
    }
}
