using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Travelable : MonoBehaviour {

    public List<string> Connections;

    public Button yourButton;

    //public Vector2 Position;
    public string Type;
    public string Name;

    public bool IsPlayerHere;

    public int Difficulty;

    public bool WasVisited;

    public GameObject PlayerShipUI;

    private Vector2 Destination;


    public void Initialize(string Name, List<string> Connections,
        int Difficulty, bool IsPlayerHere)
    {
        this.Name = Name;
        this.WasVisited = false;
        this.Difficulty = Difficulty;
        this.IsPlayerHere = IsPlayerHere;
    }

    public void OnMouseDown()
    {
        SolarSystemManager script = Camera.main.GetComponent<SolarSystemManager>();
        script.GetPlayerShipUI();

        Destination = gameObject.transform.position;
        Destination.y = Destination.y - 1.75f;

        script.SetPlayerShipUI(Destination);
        //SceneManager.LoadScene("HostileEncounter");
    }

    private void TravelHere()
    {
        if(PlayerShipUI.GetComponent<Rigidbody2D>().IsSleeping())
        {
            Vector2.MoveTowards(transform.position, Destination, 10 * Time.deltaTime);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        TravelHere();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")//check the tag of the obj collided with
        {
            SceneManager.LoadScene("HostileEncounter");
        }

        if (other.gameObject.tag == "Exit")
        {
            //SceneManager.LoadScene("SolarSystemNavigation");
        }
    }
}

