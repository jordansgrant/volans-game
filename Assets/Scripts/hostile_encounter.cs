using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hostile_encounter : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;

    public GameObject playerSpawn;

    public string ship_type;

    void Awake()
    {
        ship_type = "battleship";
        player = (GameObject)Resources.Load(@"Ships/" + ship_type);
    }
    // Use this for initialization
    void Start () {
        playerSpawn = GameObject.Find("PlayerSpawn");
        player = Instantiate(player, playerSpawn.transform.position, playerSpawn.transform.rotation);
	}

    // Update is called once per frame
    void Update () {
		
	}
}
