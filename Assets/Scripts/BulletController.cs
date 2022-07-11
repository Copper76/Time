using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject corpse;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Story" && other.gameObject.tag != "Bullet")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Enemy")
        {
            Instantiate(corpse, other.gameObject.transform.position, other.gameObject.transform.rotation);
            Destroy(other.gameObject);
        }
    }
}
