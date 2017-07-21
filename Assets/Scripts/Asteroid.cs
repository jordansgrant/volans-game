using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    Rigidbody2D asteroid;
    // Large = 0, Med = 1, Small = 2
    public int size;

    private Dictionary<string, List<GameObject>> asteroids;
    private string asteroid_type;

    private int maxVelocity;
	// Use this for initialization
	void Start () {
        hostile_encounter h = (hostile_encounter)GameObject.Find("Main Camera").GetComponent("hostile_encounter");
        asteroids = h.asteroids;
        asteroid_type = h.asteroid_type;

        asteroid = GetComponent<Rigidbody2D>();

        maxVelocity = 150;
    }

    // Update is called once per frame
    void Update () {
		
	}


    void OnTriggerEnter2D(Collider2D collision)
    {
        var type = collision.gameObject.GetComponent("collide_type") as collide_type;

        switch (type.type)
        {
            case "projectile":
                breakup(collision);
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var type = collision.gameObject.GetComponent("collide_type") as collide_type;

        switch (type.type)
        {
            case "asteroid_large":
                break;
            case "asteroid_med":
                break;
            case "asteroid_small":
                break;
            case "ship":
                break;
        }
    }

    private void reflect(Collider2D col)
    {
        Debug.Log("Asteroid collistion reflect");
        Rigidbody2D collider = col.GetComponent<Rigidbody2D>();
        float accelCoefficient = 1.0f - (asteroid.velocity.magnitude / maxVelocity);
        asteroid.AddForce(collider.transform.up * 200 * accelCoefficient);

    }

    private void breakup(Collider2D col)
    {
        if (size < 2)
        {
            GameObject a;
            size++;
            a = Instantiate(asteroids[asteroid_type][size], transform.position, Quaternion.identity);
            a.GetComponent<Rigidbody2D>().AddForce(col.GetComponent<Rigidbody2D>().transform.up * 200);
            ((Asteroid)a.GetComponent("Asteroid")).size = size;
            a = Instantiate(asteroids[asteroid_type][size], transform.position, Quaternion.identity);
            a.GetComponent<Rigidbody2D>().AddForce(col.GetComponent<Rigidbody2D>().transform.right * 200);
            ((Asteroid)a.GetComponent("Asteroid")).size = size;

        }
        Destroy(gameObject);
    }
}
