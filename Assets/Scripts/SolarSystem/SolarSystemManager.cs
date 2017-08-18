using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SolarSystemManager : MonoBehaviour
{
    //Number actually spawned
    public int NumberOfTravelables;

    public GameObject PlayerShipUI;
    public GameObject EnemyFleet;
    public GameObject Gate;

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

    public Vector2 SetGatePosition(Vector3 position)
    {
        return Gate.transform.position = position;
    }

    int TotalMoves; // Total moves made by the player
    int PlayerPosition; // Travelable ID where player is currently at

    private List<string> Names =
         new List<string> {"Nobreinia", "Padraurus", "Ratune", "Cuchov",
             "Vewhapus", "Glokutis", "Smowanope", "Clillon 9U5M", "Gromia N0F", "Stov HN3",
        "Sinq Laison", "Jita", "Gaia", "Phaedrus", "Aridia", "Adia", "Amod", "Niajara", "HED-GP",
         "Rancer", "Ami", "Fensi", "Amodonen", "Dodixie", "Egghelande", "Vylade", "Ney", "Goinard",
         "Jel", "Angur", "Antem", "Kino", "Nani", "Ruvas"};

    //private List<string> UsedNames;

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

    void ActivateFittedModules()
    {
        foreach (var mod in GameManager.game.pData.moduleAttached)
        {
            GameObject module = Resources.Load("Modules/" + mod) as GameObject;
            module.GetComponent<IModule>().addEffect();
        }
    }

    void DeactivateBoxColliders()
    {
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }
    //Spawn a random number of travelables in random positions
    void SpawnTravelables()
    {    
        float rightBound = 15.0f;
        float leftBound = -13.0f;
        float topBound = 7.00f;
        float botBound = -6.0f;

        var SolarSystem = GameManager.game.sData;
        colliders = new List<BoxCollider2D>();

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
           
            if (collisionDetected(currCollider))
            {
                SolarSystem.UsedNames.Remove(currentName);//Remove currentName from used names
                Names.Add(currentName);//re-add currentName to unused name pool upon destruction
                Destroy(TravelableObject);
                i = i - 1; //Redo this iteration.
            }
            else
            {
                colliders.Add(currCollider);

                //Save current planet in game manager
                newPlanet.Name = currentName;
                //print("newPlanet.Name " + newPlanet.Name);
                //print(newPlanet.Name);
                newPlanet.PreFabNum = prefabNum;
                newPlanet.Difficulty = difficulty;
                newPlanet.Position = position;
                newPlanet.Tag = (string)TravelableObject.tag;
                newPlanet.wasVisited = false;
                SolarSystem.PlanetsData[newPlanet.Name] = newPlanet;
                
            }
        }

        DeactivateBoxColliders();

        float xGate = Random.Range(15.8f, 16.8f);
        float yGate = Random.Range(-5.5f, 9.0f);

        Vector3 gatePos = SetGatePosition(new Vector3(xGate, yGate, 0f));
        Gate.transform.position = gatePos;

        Gate.GetComponent<SpriteRenderer>().enabled = true;
        GameManager.game.sData.gatePosition = gatePos;
    }

    //Load an already existing solar system.
    void LoadSolarSystem()
    {
        LoadPlanets();
        LoadEnemyFleet();
        //deactivate box colliders

        Gate.transform.position = GameManager.game.sData.gatePosition;
        Gate.GetComponent<SpriteRenderer>().enabled = true;
    }

    void LoadEnemyFleet()
    {
        //
        float scale = (GameManager.game.sData.Turn * 2.75f);
        Vector3 FleetWidth = new Vector3(scale, 20f, 0f);

        EnemyFleet.transform.localScale = FleetWidth;
    }

    void LoadPlanets()
    {
        var Planets = GameManager.game.sData.PlanetsData;
        //colliders = new List<BoxCollider2D>();

        print("planet count: " + Planets.Count);

        foreach (var currentPlanet in Planets.Values)
        {
            GameObject TravelableObject;
            GameObject currentPlanetPrefab;

            currentPlanetPrefab = Resources.Load(@"Travelables\" + Prefabs[currentPlanet.PreFabNum]) as GameObject;

            //Instantiate
            TravelableObject = Instantiate(currentPlanetPrefab, currentPlanet.Position, Quaternion.identity);
            
            //Initialize script
            Travelable Script = TravelableObject.GetComponent<Travelable>();
            Script.Initialize(currentPlanet.Name, difficulty);
            Script.WasVisited = currentPlanet.wasVisited;

            //Deactivate collider
            TravelableObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
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
        //Select solar system scene
        Scene scene = SceneManager.GetActiveScene();
        GameManager.game.sData.Level = scene.name;
        if (!GameManager.game.sData.isSpawned)
        {
            ActivateFittedModules();
            SpawnTravelables();
            GameManager.game.sData.isSpawned = true;
        }
        else
        {
            ActivateFittedModules();
            LoadSolarSystem();
        }  
    }

    void Update()
    {


    }

}
