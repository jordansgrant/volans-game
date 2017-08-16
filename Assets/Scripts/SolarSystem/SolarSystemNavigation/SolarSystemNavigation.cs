using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SolarSystemNavigation : MonoBehaviour {
    private float speed = 5;       //how fast the player will move
    private Vector3 targetPosition; //player position
    private bool isMoving;          //check if player is moving
    private bool isAtPlanet;
    public Vector2 currentPosition;
    const int LEFT_MOUSE_BUTTON = 0;//is the mouse clicked
    const float range = 8.0f;

    private Vector3 scaleFactor;
    private float FleetWidth = 0;
    public GameObject EnemyFleet;

    // Use this for initialization
    void Start ()
    {
        targetPosition = transform.position;
        if (GameManager.game.sData.isStartingPosition == true)
        {
            print("hello");
            GameManager.game.sData.playerPosition = new Vector2(-17.0f, 0.0f);
            transform.position = GameManager.game.sData.playerPosition;
            //GameManager.game.sData.isStartingPosition = false;
        }
        else
        {
            print("reload ship pos");
            print(GameManager.game.sData.playerPosition);
            transform.position = GameManager.game.sData.playerPosition;
        }

        isMoving = false;
        isAtPlanet = false;
    }

    // Update is called once per frame
    void Update ()
    {
        //if the mouse button has been pressed and ship is not moving, set the target position
        if (Input.GetKeyDown(KeyCode.Mouse0) && isMoving == false)
            SetTargetPosition();

        //if the player is moving, keep moving
        if (isMoving)
            MovePlayer();

        if(!isMoving)
        {
            CheckIfInEnemy();
        }

    }

    private void CheckIfInEnemy()
    {

        //BoxCollider2D fleetCollider = EnemyFleet.GetComponent<Renderer>().bounds;
        //CircleCollider2D shipCollider = gameObject.GetComponent<CircleCollider2D>();

        //if (EnemyFleet.GetComponent<Renderer>().bounds.Intersects(gameObject.GetComponent<Renderer>().bounds))
        //{
        //    print("point is inside collider");
        //    return true;
        //}

        //return false;        
        float centerX = EnemyFleet.GetComponent<BoxCollider2D>().transform.position.x;
        float size = EnemyFleet.GetComponent<BoxCollider2D>().size.x;
        float playerPosition = gameObject.transform.position.x;

        //print(scaleFactor.x + " " + (centerX + scaleFactor.x) + " " + playerPosition);

        if (centerX + scaleFactor.x > playerPosition)
        {
            //print("point is inside collider");

        }
    }

    void SetTargetPosition()
    {
        //Prevents placing player at spawn on subsequent turns
        GameManager.game.sData.isStartingPosition = false;

        Vector3 currentPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        //Set to current mouse position
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Prevent movement on click of a menu element.
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        float centerX = EnemyFleet.GetComponent<BoxCollider2D>().offset.x;
        float size = EnemyFleet.GetComponent<BoxCollider2D>().size.x;

        if (currentPosition.x + range < targetPosition.x)
        {
            SolarSystemGUI.instance.DisplayNotification("That destination is too far away. Please make another selection.");
            return;
        }

        targetPosition.z = transform.position.z;
        if (isMoving == false)
            isMoving = true;

        //Increment Turn
        GameManager.game.sData.Turn++;
        print(GameManager.game.sData.Turn);

        ExpandHostileArea();
      
    }

    private void ExpandHostileArea()
    {
        scaleFactor = new Vector3(2.75f, 0.0f);

        if (GameManager.game.sData.Turn < 1)
            scaleFactor = new Vector3(4.5f, 0.0f);

        EnemyFleet.transform.localScale += scaleFactor;       
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
            currentPosition = transform.position;
            GameManager.game.sData.playerPosition = currentPosition;

            if (isAtPlanet == false)
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
