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
    void SpawnTravelable()
    {    
        float rightBound = -4.0f;
        float leftBound = -16.0f;
        float topBound = 9.0f;
        float botBound = 1.0f;
        float horzOffset = 0f;
        float vertOffset = 0f;

        for(int j = 0; j < 2; j++)
        {
            for (int i = 0; i < NumberOfTravelables; i++)
            {
                GameObject TravelableObject;

                //Generate random position
                float x = Random.Range(leftBound += horzOffset,
                    rightBound += horzOffset);
                float y = Random.Range(topBound + vertOffset, botBound + vertOffset);

                TravelableObject = Instantiate(Travelables[UsedNames[i]][0], new Vector2(x, y), Quaternion.identity);

                Travelable Script = TravelableObject.GetComponent<Travelable>();

                //Check here if new object collides with an existing object

                List<string> Connections = new List<string> { "test" };

                if (i == 0)
                {
                    Script.Initialize(UsedNames[i], Connections,
                         1, true);
                }
                else
                {
                    Script.Initialize(UsedNames[i], Connections,
                         1, true);
                }

                if (i == NumberOfTravelables - 1)
                {
                    TravelableObject.tag = "Exit";
                }
                else
                {
                    TravelableObject.tag = "Anomaly";
                }

                horzOffset += 3;
            }

            vertOffset -= 6;
            horzOffset = 0f;
        }
        
    }

    // Use this for initialization
    void Awake ()
    {
        Travelables = new Dictionary<string, List<GameObject>>();

        UsedNames = new List<string> { };

        GameObject PlayerShipUI = new GameObject();

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
        SpawnTravelable();
        
    }
    
    // Update is called once per frame
    void Update ()
    {

    }
}
