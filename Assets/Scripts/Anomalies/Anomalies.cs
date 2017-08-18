using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Anomalies : MonoBehaviour {
    //for random number generation
    System.Random rnd;
    
    //player ship
    public GameObject player;
    //public GameObject playerSpawn;

    //other ship
    public GameObject other;

    //Canvas text field and buttons
    public GameObject anomalyText;
    public GameObject button1;
    public GameObject button2;

    //TODO: replace with a function that fills the list from a text file
    //list of text blocks for potential anomaly encounters
    private List<string> encounterText = new List<string>
    {
        "An unidentified ship has opened hailing frequencies. They contracted a stange disease and are asking for help",
        "We have spotted an asteroid with an unusual metal",
    };

    //TODO: replace with a function that fills the list from a text file
    //list of text blocks for responses
    private List<string> responseText = new List<string>
    {
        "Let's help them out",
        "Destory them and lets move on",
        "Let's take some time to mine what we can",
        "We have no time for this - scan it for later analysis and let's move on"
    };

    // Use this for initialization
    void Start () {
        Debug.Log("@1");
        //decide on the anomaly type
        rnd = new System.Random();
        int anomalyEvent = rnd.Next(2); //between 0 and 1
        int responeChoices = anomalyEvent * 2;

        //assign text objects to ones in the scene
        anomalyText = GameObject.Find("AnomalyText");
        Text aText = anomalyText.GetComponent<Text>();
        button1 = GameObject.Find("Choice1");
        button2 = GameObject.Find("Choice2");

        //fill in text objects
        switch(anomalyEvent)
        {
            case 0:
                aText.text = encounterText[0];
                break;
            case 1:
                aText.text = encounterText[0];
                break;
        }
        
        //create the player ship
        //playerSpawn = GameObject.Find("PlayerSpawn");
        player = GameObject.Find("PlayerSpawn");
        player = (GameObject)Resources.Load(@"Ships/battleship");
        //string ship_type = PlayerPrefs.GetString("ship_type");
        //player = (GameObject)Resources.Load(@"Ships/" + ship_type);
        player = Instantiate(player, player.transform.position, player.transform.rotation);
        Debug.Log(player.transform.position);



    }

    // Update is called once per frame
    void Update () {
		
	}
}
