using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class bullet_script : MonoBehaviour {

    public GameObject explosion;

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(explosion, collision.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
