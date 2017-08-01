using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    private SpriteRenderer shield;
    private CircleCollider2D circleCollider;

    public bool isEnabled = false;

    private float shieldTickRate = .05f;
    private float lastTick = 0.0f;

    // Use this for initialization
    void Start () {
        shield = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();

        disable();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("c") && player_ship.power > 0)
        {
            if (!isEnabled)
                enable();

            if (Time.time > lastTick + shieldTickRate)
            {
                player_ship.power -= 10;
                lastTick = Time.time;
            }
        }
        else if (isEnabled)
            disable();
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        collide_type type = collision.GetComponent<collide_type>();
        switch (type.type)
        {
            case "projectile":
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collide_type type = collision.collider.GetComponent<collide_type>();
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
            case "shield":
                break;
        }
    }

    private void disable()
    {
        shield.enabled = false;
        circleCollider.isTrigger = true;
        isEnabled = false;
    }

    private void enable()
    {
        shield.enabled = true;
        circleCollider.isTrigger = false;
        isEnabled = true;
    }
}
