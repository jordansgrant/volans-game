using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemManager : MonoBehaviour
{
    //Number actually spawned
    public int NumberOfTravelables;

    public GameObject PlayerShipUI;

    private List<GameObject> PlayerInventory;
    private List<BoxCollider2D> colliders;

    private int difficulty = 1;

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
        "Sinq Laison", "Jita", "Gaia", "Phaedrus", "Aridia", "Adia", "Amod", "Niajara", "HED-GP",
         "Rancer", "Ami", "Fensi", "Amodonen", "Dodixie", "Egghelande", "Vylade", "Ney", "Goinard",
         "Jel", "Angur", "Antem", "Kino", "Nani", "Ruvas"};

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
    void SpawnTravelables()
    {    
        float rightBound = 15.5f;
        float leftBound = -13.0f;
        float topBound = 7.00f;
        float botBound = -6.0f;

        var SolarSystem = GameManager.game.sData;
        colliders = new List<BoxCollider2D>();

        UsedNames = new List<string> { };

        for (int i = 0; i < NumberOfTravelables; i++)
        {
            GameObject TravelableObject;
            GameObject currentPlanet;
            SolarSystem.PlanetData newPlanet = new SolarSystem.PlanetData();

            string currentName = GetUniqueRandomName();

            SolarSystem.UsedNames.Add(currentName);

            int prefabNum = Random.Range(0, 7);
            currentPlanet = Resources.Load(@"Travelables\" + Prefabs[prefabNum]) as GameObject;

            //Generate random position
            float x = Random.Range(leftBound, rightBound);
            float y = Random.Range(topBound, botBound);
            Vector2 position = new Vector2(x, y);


            TravelableObject = Instantiate(currentPlanet, position, Quaternion.identity);
            TravelableObject.tag = "Anomaly";
            Travelable Script = TravelableObject.GetComponent<Travelable>();
            BoxCollider2D currCollider = TravelableObject.GetComponentInChildren<BoxCollider2D>();
       
            //Initialize Travelable Object's Traits
            Script.Initialize(SolarSystem.UsedNames[i], difficulty);
            

            if (i == NumberOfTravelables - 1)
            {
                TravelableObject.tag = "Exit";
            }

            if (collisionDetected(currCollider))
            {
                Names.Add(currentName);
                Destroy(TravelableObject);
                i = i - 1; //Redo this iteration.
            }
            else
            {
                colliders.Add(currCollider);

                //print("Spawn: " + i);

                //Save current planet in game manager
                newPlanet.Name = currentName;
                newPlanet.PreFabNum = prefabNum;
                newPlanet.Difficulty = difficulty;
                newPlanet.Position = position;
                newPlanet.Tag = (string)TravelableObject.tag;
                newPlanet.wasVisited = false;
                SolarSystem.PlanetsData[currentName] = newPlanet;
            }
        }
    }

    //Load an already existing solar system.
    void LoadSolarSystem()
    {
        LoadPlanets();
    }

    void LoadPlanets()
    {
        var Planets = GameManager.game.sData.PlanetsData;
        colliders = new List<BoxCollider2D>();
        print(GameManager.game.sData.isSpawned);

        print("planet count: " + Planets.Count);

        foreach (var currentPlanet in Planets.Values)
        {
            GameObject TravelableObject;
            GameObject currentPlanetPrefab;

            currentPlanetPrefab = Resources.Load(@"Travelables\" + Prefabs[currentPlanet.PreFabNum]) as GameObject;

            print(currentPlanet.Name);

            //Instantiate
            TravelableObject = Instantiate(currentPlanetPrefab, currentPlanet.Position, Quaternion.identity);
            
            //Initialize script
            Travelable Script = TravelableObject.GetComponent<Travelable>();
            Script.Initialize(currentPlanet.Name, difficulty);
            print(currentPlanet.wasVisited);
            Script.WasVisited = currentPlanet.wasVisited;

            //Add collider
            BoxCollider2D currCollider = TravelableObject.GetComponentInChildren<BoxCollider2D>();
            colliders.Add(currCollider);
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

    void Start ()
    {
        if(!GameManager.game.sData.isSpawned)
        {
            //newSolarSystem();
            SpawnTravelables();
            GameManager.game.sData.isSpawned = true;
        }
        else
        {
            LoadSolarSystem();
            print("reload");
        }  
    }
    
}
