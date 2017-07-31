using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SolarSystemNavigation : MonoBehaviour {
    private float speed = 10;       //how fast the player will move
    private Vector3 targetPosition; //player position
    private bool isMoving;          //check if player is moving
    const int LEFT_MOUSE_BUTTON = 0;//is the mouse clicked
    const float range = 8.0f;
    // Use this for initialization
    void Start ()
    {
        targetPosition = transform.position;
        isMoving = false;
    }
    
    // Update is called once per frame
    void Update ()
    {
        //if the mouse button has been pressed and ship is not moving, set the target position
        if (Input.GetMouseButton(LEFT_MOUSE_BUTTON) && isMoving == false)
            SetTargetPosition();

        //if the player is moving, keep moving
        if (isMoving)
            MovePlayer();

    }

    void SetTargetPosition()
    {
        //check that the target position is valid - may bave to be through an edge matrix
        Vector2 currentPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        
        //if valid, set to current mouse position
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if(currentPosition.x + range < targetPosition.x)
        {
            return;
        }

        targetPosition.z = transform.position.z;
        if (isMoving == false)
            isMoving = true;

    }

    void MovePlayer()
    {
        //move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        //stop if you're there
        if (transform.position == targetPosition)
            isMoving = false;
    }
}
