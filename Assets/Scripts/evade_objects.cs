using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class evade_objects : MonoBehaviour
{
    public float speed = 2;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = new RaycastHit2D();
        hit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, 
            1 << LayerMask.NameToLayer("Foreground"));

        if (hit.distance < 2f)
        {
            MoveAway(hit.transform.position);
        }
        

    }

    private void MoveAway(Vector3 position)
    {
        Vector3 dir = position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle - 180, Vector3.forward);
        // perform rotation slowly toward player 
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);


        //move towards the player
        if (Vector3.Distance(transform.position, position) > 1f)
        {//move if distance from target is greater than 1
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
    }
}
