using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public GameManager gm;
    public float offset = 0;
    private float time_down = 0;

    private void LateUpdate()
    {
        if (gm.isActive && !(gm.isPaused))
        {
            if (target.position.y > transform.position.y + offset)
            {
                Vector3 new_pos = new Vector3(transform.position.x,
                target.position.y - offset, transform.position.z);

                transform.position = new_pos;
            }
            time_down = Time.time;
        }
    }

    private void FixedUpdate()
    {
        if (!(gm.isActive))
        {
            if (Time.time < time_down + 2)
                transform.position -= new Vector3(0, 30 * Time.deltaTime, 0);
        }
    }
}
