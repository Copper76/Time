using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public Vector3[] waypoints;

    private Rigidbody2D rb;
    private int waypointIndex;
    public float dist;
    private Vector3 rbpos;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        waypointIndex = -1;
        IncreaseIndex();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex]);
        if (dist < 0.1f)
        {
            IncreaseIndex();
        }
        Move();
    }

    void Move()
    {
        Vector3 dir = new Vector3(0, 1, 0);
        transform.Translate(dir * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        waypointIndex = (waypointIndex + 1) % waypoints.Length;
        rbpos = rb.position;
        Vector2 lookDir = waypoints[waypointIndex] - rbpos;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
