using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_script : MonoBehaviour {

    public float speed = 1;
    public Rigidbody2D bullet;
    //public Vector3 direction;
    public Vector3 direction;

	// Use this for initialization
	void Start () {

        GameObject ship = GameObject.Find("player");
        Transform shipTransform = ship.transform;
        // get player position
        Vector2 position = shipTransform.position;
        transform.position = new Vector2(position.x, position.y);
        direction = ship.transform.up;
        transform.rotation = ship.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed);
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
