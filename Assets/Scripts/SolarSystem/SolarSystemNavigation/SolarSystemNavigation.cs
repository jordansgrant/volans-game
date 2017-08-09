using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SolarSystemNavigation : MonoBehaviour {
    private float speed = 5;       //how fast the player will move
    private Vector3 targetPosition; //player position
    private bool isMoving;          //check if player is moving
    private bool isAtPlanet;
    const int LEFT_MOUSE_BUTTON = 0;//is the mouse clicked
    const float range = 8.0f;

    // Use this for initialization
    void Start ()
    {
        targetPosition = transform.position;
        isMoving = false;
        isAtPlanet = false;
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
            SolarSystemGUI.instance.DisplayNotification("That destination is too far away. Please make another selection.");
            return;
        }

        targetPosition.z = transform.position.z;
        if (isMoving == false)
            isMoving = true;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Anomaly")
        {
            isAtPlanet = true;
        }
        else
        {
            isAtPlanet = false;
        }
    }

    void MovePlayer()
    {
        //move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        SolarSystemGUI.instance.DisplayNotification("En route.");

        //stop if you're there
        if (transform.position == targetPosition)
        {
            if(isAtPlanet == false)
            {
                SolarSystemGUI.instance.DisplayNotification("Choose a new destination.");
                isMoving = false;
            }
            else if(isAtPlanet == true)
            {
                SolarSystemGUI.instance.DisplayNotification("You have already visited this planet.");
                isMoving = false;
            }

        }
    }
}
