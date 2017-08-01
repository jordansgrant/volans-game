using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class bullet_script : MonoBehaviour {

    public GameObject explosion;

    public float speed;

    void Start()
    {
        speed = 25f;
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        collide_type type = collision.GetComponent<collide_type>();
        if (type.type == "shield")
        {
            Debug.Log("Bullet hit shield!");
            return;
        }
        Instantiate(explosion, collision.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    

}
