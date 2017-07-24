using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemManager : MonoBehaviour
{

    //private List<Travelable> SolarSystem;
    //public Dictionary<string, Dictionary<string, GameObject>> SolarSystem;

    //Stores possible travelable prefabs
    public Dictionary<string, List<GameObject>> Travelables;

    //Store actual spawned travelables
    public Dictionary<string, List<GameObject>> SolarSystem;

    public int NumberOfTravelables;

    public GameObject PlayerShipUI;

    public GameObject GetPlayerShipUI()
    {
        return PlayerShipUI;
    }

    public void SetPlayerShipUI(Vector3 position)
    {
        this.PlayerShipUI.transform.position = position;
    }

    int TotalMoves; // Total moves made by the player
    int PlayerPosition; // Travelable ID where player is currently at

    private List<string> Names =
         new List<string> {"Nobreinia", "Padraurus", "Ratune", "Cuchov",
             "Vewhapus", "Glokutis", "Smowanope", "Clillon 9U5M", "Gromia N0F", "Stov HN3",
        "Sinq Laison", "Jita", "Gaia", "Phaedrus"};

    private List<string> UsedNames;

    private List<string> Prefabs = new List<string> { "gaia", "island",
    "jungle", "lava", "ocean", "rock", "toxic", "volcano"};


    private string GetUniqueRandomName()
    {
        System.Random rand = new System.Random();
        int index = rand.Next(Names.Count);

        string name = Names[index];
        Names.RemoveAt(index);

        return name;
    }

    //Spawn a random number of travelables in random positions
    bool SpawnTravelable(int count, float horzOffset)
    {
        GameObject TravelableObject;

        float rightBound = 13.0f;
        float leftBound = -10.5f;

        if (count == 0) {
            rightBound = -10.5f; // Spawn near left side of screen            
        }
        if(count == NumberOfTravelables - 1) {
            leftBound = 10.5f; // Spawn near right side of screen
        }

        float x = Random.Range(leftBound, rightBound);
        float y = Random.Range(-9.0f, 9.0f);

        TravelableObject = Instantiate(Travelables[UsedNames[count]][0], new Vector2(x, y), Quaternion.identity);
        //TravelableObject = Instantiate(canvas, new Vector2(x, y), Quaternion.identity);

        Travelable Script = TravelableObject.GetComponent<Travelable>();

        List<string> Connections = new List<string> { "test" };

        if (count == 0)
        {
            Script.Initialize(UsedNames[count], Connections,
                 1, true);
            PlayerShipUI = Instantiate(PlayerShipUI, new Vector2(x, (y - 1.75f)), Quaternion.identity);
        }
        else
        {
            Script.Initialize(UsedNames[count], Connections,
                 1, true);
        }



        if (count == NumberOfTravelables - 1)
        {
            TravelableObject.tag = "Exit";
        }
        else
        {
            TravelableObject.tag = "Anomaly";
        }
        return true;
    }

    // Use this for initialization
    void Awake ()
    {
        Travelables = new Dictionary<string, List<GameObject>>();

        UsedNames = new List<string> { };

        GameObject PlayerShipUI = new GameObject();
        //PlayerShipUI = (GameObject)(Resources.Load(@"UI\" + "PlayerShipUI"));
        //Debug.Log(PlayerShipUI);

        for (int i = 0; i < NumberOfTravelables; i++)
        {
            string currentName = GetUniqueRandomName();

            UsedNames.Add(currentName);

            Travelables.Add(currentName, new List<GameObject>());

            Travelables[UsedNames[i]].Add(Resources.Load(@"Travelables\" +
                Prefabs[Random.Range(0, 7)]) as GameObject);
        }

    }

    void Start ()
    {
        float horzOffset = 0f;
        int count = 0;
        do
        {
            if (SpawnTravelable(count, horzOffset))
            {
                count++;
                horzOffset += .5f;
            }
                

        } while (count < NumberOfTravelables);
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
