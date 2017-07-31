using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_find_player : MonoBehaviour {
    
    public float speed = 2;
    public float rotationSpeed = 2f;
    private Transform player;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            GameObject oPlayer = GameObject.FindGameObjectWithTag("Player");

            if (oPlayer == null)
                return;

            player = oPlayer.transform;
        }

        if (player != null)
        {
            MoveTowards(player.position);
        }
	}

    public void MoveTowards(Vector3 position)
    {
  
        Vector3 dir = position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        // perform rotation slowly toward player 
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);

        
        //move towards the player
        if (Vector3.Distance(transform.position, position) > 1f)
        {//move if distance from target is greater than 1
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
    }
}
