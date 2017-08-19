using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Physicall Body of the asteroid
    Rigidbody2D asteroid;
    
    // Asteroid size, used to determine if a breakup is needed
    // Large = 0, Med = 1, Small = 2
    public int size;

    // Used to store asteroid prefabs for quick access
    private Dictionary<string, List<GameObject>> asteroids;
    // stores the color of the current asteroid
    private string asteroid_type;

    // Use this for initialization
    void Start()
    {
        // try to get asteroid list from Hostile encounter
        HostileEncounter h = (HostileEncounter)GameObject.Find("Main Camera").GetComponent("HostileEncounter");
        if (h != null)
        {
            asteroids = h.asteroids;
            asteroid_type = h.asteroid_type;
        }
        // If this is a Boss Encounter get list from BossEncounter
        else
        {
            BossEncounter b = (BossEncounter)GameObject.Find("Main Camera").GetComponent("BossEncounter");
            asteroids = b.asteroids;
            asteroid_type = b.asteroid_type;
        }
        // Get asteroids physical body
        asteroid = GetComponent<Rigidbody2D>();
    }

    // breakup asteroid on collision with projectile
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the collision type of the object that hit the asteroid
        CollideType type = collision.gameObject.GetComponent("CollideType") as CollideType;
        switch (type.type)
        {
            // if its a projectile trigger a breakup
            case "projectile":
                breakup(collision);
                break;
        }
    }
    
    // Breaks up an asteroid if and destroys the previous asteroid
    // If the asteroid is small it is just destroyed
    private void breakup(Collider2D col)
    {
        // If the asteroid is size 2 (small) dont breakup just destroy the original
        if (size < 2)
        {
            // temporarily story new asteroid
            GameObject a;
            // set size to one smaller than current asteroid
            size++;
            // Get the projectiles rigidbody
            Rigidbody2D colliderBody = col.GetComponent<Rigidbody2D>();

            // Instantiate the first asteroid and make it move in the projectiles up direction
            a = Instantiate(asteroids[asteroid_type][size], transform.position, Quaternion.identity);
            a.GetComponent<Rigidbody2D>().velocity = col.transform.up * 10;
            // set its size
            ((Asteroid)a.GetComponent("Asteroid")).size = size;

            // need to instantiate a little bit away so that they do not collide and mess up the breakup
            a = Instantiate(asteroids[asteroid_type][size], new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f), Quaternion.identity);
            // the second asteroid goes in the right direction of the projectile
            a.GetComponent<Rigidbody2D>().velocity = col.transform.right * 10;
            ((Asteroid)a.GetComponent("Asteroid")).size = size;

        }
        Destroy(gameObject);
    }
}
