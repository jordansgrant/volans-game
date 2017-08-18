﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    Rigidbody2D asteroid;
    // Large = 0, Med = 1, Small = 2
    public int size;

    private Dictionary<string, List<GameObject>> asteroids;
    private string asteroid_type;

    // Use this for initialization
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        HostileEncounter h = (HostileEncounter)GameObject.Find("Main Camera").GetComponent("HostileEncounter");
        if (h != null)
        {
            asteroids = h.asteroids;
            asteroid_type = h.asteroid_type;
        }
        else
        {
            BossEncounter b = (BossEncounter)GameObject.Find("Main Camera").GetComponent("BossEncounter");
            asteroids = b.asteroids;
            asteroid_type = b.asteroid_type;
        }

        asteroid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // breakup asteroid on collision with projectile
    void OnTriggerEnter2D(Collider2D collision)
    {
        CollideType type = collision.gameObject.GetComponent("CollideType") as CollideType;
        Debug.Log(type.type);
        switch (type.type)
        {
            case "projectile":
                breakup(collision);
                break;
        }
    }
    

    private void breakup(Collider2D col)
    {
        if (size < 2)
        {
            GameObject a;
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
