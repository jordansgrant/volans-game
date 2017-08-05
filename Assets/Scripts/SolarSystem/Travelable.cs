using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Travelable : MonoBehaviour {

    public List<string> Connections;

    public string Type;
    public string Name;
    public bool IsPlayerHere;
    public int Difficulty;
    public bool WasVisited;

    public IModule Module;
    public GameObject PlayerShipUI;

    //Put slot to hold a module

    private Vector2 Destination;

    public void Initialize(string Name, List<string> Connections,
        int Difficulty, bool IsPlayerHere, IModule Module = null)
    {
        this.Name = Name;
        this.WasVisited = false;
        this.Difficulty = Difficulty;
        this.IsPlayerHere = IsPlayerHere;
        this.Module = Module;
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
        print("collider type" + other.GetType());
        if (other.gameObject.tag == "Player")//check the tag of the obj collided with
        {
            print(WasVisited);
            WasVisited = true;
            //SceneManager.LoadScene("HostileEncounter");
        }

        if (other.gameObject.tag == "Exit")
        {
            //SceneManager.LoadScene("SolarSystemNavigation");
        }
    }
}

