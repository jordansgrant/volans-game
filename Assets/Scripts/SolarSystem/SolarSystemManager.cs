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

    //Number actually spawned
    public int NumberOfTravelables;

    public GameObject PlayerShipUI;

    private List<GameObject> PlayerInventory;
    private List<BoxCollider2D> colliders;

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
        float rightBound = 15.5f;
        float leftBound = -13.0f;
        float topBound = 7.00f;
        float botBound = -6.0f;
        float horzOffset = 0f;
        float vertOffset = 0f;

        for(int i = 0; i < NumberOfTravelables; i++)
        {
            GameObject TravelableObject;

            //Generate random position
            float x = Random.Range(leftBound, rightBound);
            float y = Random.Range(topBound, botBound);

            TravelableObject = Instantiate(Travelables[UsedNames[i]][0], new Vector2(x, y), Quaternion.identity);

            Travelable Script = TravelableObject.GetComponent<Travelable>();
            BoxCollider2D currCollider = TravelableObject.GetComponentInChildren<BoxCollider2D>();

            //Check here if new object collides with an existing object

            List<string> Connections = new List<string> { "test" };
       
            //Initialize Travelable Object's Traits
            Script.Initialize(UsedNames[i], Connections, 1, true);
                

            if (i == NumberOfTravelables - 1)
            {
                TravelableObject.tag = "Exit";
            }
            else
            {
                TravelableObject.tag = "Anomaly";
            }            

            if (collisionDetected(currCollider))
            {
                print("Destroyed");
                Destroy(TravelableObject);
                i = i - 1; //Redo this iteration.
            }
            else
            {
                colliders.Add(currCollider);

                //Save current planet in game manager
                //Debug.Log(TravelableObject.GetComponent<Travelable>().Name);
                //print(Script.name + " " + TravelableObject.transform.position.x);
                GameManager.game.sData.Planets.Add(TravelableObject.GetComponent<Travelable>().Name, TravelableObject);
                print("from gamemanager " + GameManager.game.sData.Planets[TravelableObject.GetComponent<Travelable>().Name]);
                print("planet count: " + GameManager.game.sData.Planets.Count);
            }
        }
    }

    private bool collisionDetected(BoxCollider2D spawn)
    {
        foreach (BoxCollider2D col in colliders)
        {
            if (col.bounds.Intersects(spawn.bounds))
                return true;
        }
        return false;
    }
    // Use this for initialization
    void Awake ()
    {
        Travelables = new Dictionary<string, List<GameObject>>();
        colliders = new List<BoxCollider2D>();

        //Debug.Log(GameManager.game.sData);

       UsedNames = new List<string> { };

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
        SpawnTravelable();        
    }
    
    // Update is called once per frame
    void Update ()
    {

    }
}
