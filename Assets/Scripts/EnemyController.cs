using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float bulletSpeed;
    public GameObject bullet;

    private Rigidbody2D rb;
    private float angle;
    private Vector2 playerPos;
    private float fireRate;
    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fireRate = 3f;
        nextFire = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void FixedUpdate()
    {
        Vector2 lookDir = playerPos - rb.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void Shoot()
    {
        GameObject b = Instantiate(bullet, this.transform.position, transform.rotation);
        b.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (angle + 90)), Mathf.Sin(Mathf.Deg2Rad * (angle + 90)), 0) * bulletSpeed;
    }
}
