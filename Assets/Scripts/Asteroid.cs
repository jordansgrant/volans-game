using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    Rigidbody2D asteroid;
    // Large = 0, Med = 1, Small = 2
    public int size;

    private Dictionary<string, List<GameObject>> asteroids;
    private string asteroid_type;
    
	// Use this for initialization
	void Start () {
        Random.InitState(System.DateTime.Now.Millisecond);
        hostile_encounter h = (hostile_encounter)GameObject.Find("Main Camera").GetComponent("hostile_encounter");
        asteroids = h.asteroids;
        asteroid_type = h.asteroid_type;

        asteroid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    // breakup asteroid on collision with projectile
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

    // currently no additional logic for these specific collisions
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
    

    private void breakup(Collider2D col)
    {
        if (size < 2)
        {
            GameObject a,b;
            size++;
            
            Rigidbody2D colliderBody = col.GetComponent<Rigidbody2D>();

            a = Instantiate(asteroids[asteroid_type][size], transform.position, Quaternion.identity);
            a.GetComponent<Rigidbody2D>().velocity = col.transform.up * 10;
            ((Asteroid)a.GetComponent("Asteroid")).size = size;

            // need to instantiate a little bit away so that they do not collide and mess up the breakup
            a = Instantiate(asteroids[asteroid_type][size], new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f), Quaternion.identity);
            a.GetComponent<Rigidbody2D>().velocity = col.transform.right * 10;
            ((Asteroid)a.GetComponent("Asteroid")).size = size;

        }
        Destroy(gameObject);
    }
}
