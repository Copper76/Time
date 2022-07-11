using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float bulletSpeed;
    public GameObject bullet;
    public Camera cam;

    //ui stuff
    public GameObject lossPanel;
    public GameObject storyPanel;
    public Text storyText;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 mousePos;
    private float angle;
    private bool canControl;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canControl = true;
    }

    void Update()
    {
        if (canControl)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonUp(0))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                storyPanel.SetActive(false);
                canControl = true;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canControl)
        {
            rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

            Vector2 lookDir = mousePos - rb.position;
            angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
         }
    }

    void Shoot()
    {
        GameObject b = Instantiate(bullet, this.transform.position, transform.rotation);
        b.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (angle+90)), Mathf.Sin(Mathf.Deg2Rad * (angle + 90)), 0) * bulletSpeed ;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Bullet")
        {
            lossPanel.SetActive(true);
            canControl = false;
            rb.velocity = new Vector2(0, 0);
            Debug.Log(rb.velocity);
        }

        if (other.gameObject.tag == "Story")
        {
            storyPanel.SetActive(true);
            storyText.text = other.gameObject.GetComponent<Text>().text;
            Destroy(other.gameObject);
            canControl = false;
            rb.velocity = new Vector2(0, 0);
        }
    }
}
