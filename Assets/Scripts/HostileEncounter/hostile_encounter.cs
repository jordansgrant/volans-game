using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hostile_encounter : MonoBehaviour {

    // Enemy Ship
    public GameObject enemy;

    // player ship
    public GameObject player;
    public GameObject playerSpawn;
    public string ship_type;

    // Collidables
    public Dictionary<string, List<GameObject>> asteroids;
    public string asteroid_type;

    private List<PolygonCollider2D> colliders;

    void Awake()
    {
        // Instantiate Data Storage
        asteroids = new Dictionary<string, List<GameObject>>();
        colliders = new List<PolygonCollider2D>();
        Debug.Log(GameManager.game);

        ship_type = GameManager.game.pData.shipType;
        Debug.Log(ship_type);

        player = (GameObject)Resources.Load(@"Ships/" + ship_type);

        // Setup Collidable prefabs
        asteroids.Add("Brown", new List<GameObject>());
        asteroids.Add("Grey", new List<GameObject>());

        foreach (var size in new string[3] { "Large", "Med", "Small" })
        {
            foreach (var key in asteroids.Keys)
            {
                asteroids[key].Add(Resources.Load(@"Collidables\Asteroid_" +
                                                  size + "_" + key) as GameObject);
            }
        }
    }
    // Use this for initialization
    void Start () {
        ship_type = GameManager.game.pData.shipType;
        player = (GameObject)Resources.Load(@"Ships/" + ship_type);

        // Spawn Player
        playerSpawn = GameObject.Find("PlayerSpawn");
        player = Instantiate(player, playerSpawn.transform.position, playerSpawn.transform.rotation);

        colliders.Add(player.GetComponent<PolygonCollider2D>());

        GameObject enemy = Resources.Load(@"Ships/enemy_cruiser") as GameObject;
        GameObject enemySpawn = GameObject.Find("EnemySpawn");
        Quaternion rotation = Quaternion.Inverse(enemy.transform.rotation);
        enemy = Instantiate(enemy, enemySpawn.transform.position, rotation) as GameObject;
        colliders.Add(enemy.GetComponent<PolygonCollider2D>());

        // Spawn asteroids
        asteroid_type = "Brown";
        System.Random rand = new System.Random(System.DateTime.Now.Millisecond);
        int num_asteroids = rand.Next(1, 5);

        int count = 0;
        do
        {
            if (randomAsteroidSpawn())
                count++;

        } while (count < num_asteroids);

    }

    // Update is called once per frame
    void Update () {
        
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 30), "Player Armor:  " + ((player_ship.armor < 0) ? 0 : player_ship.armor));
        GUI.Label(new Rect(10, 25, 150, 30), "Player Power:  " + ((player_ship.power < 0) ? 0 : player_ship.power));
        GUI.Label(new Rect(10, 40, 150, 30), "Enemy Armor: " + ((enemy_ship.armor < 0) ? 0 : enemy_ship.armor));
       

    }

    private bool randomAsteroidSpawn()
    {
        GameObject asteroid;

        float x = Random.Range(-13.0f, 13.0f);
        float y = Random.Range(-9.0f, 9.0f);

        asteroid = Instantiate(asteroids[asteroid_type][0], new Vector2(x, y), Quaternion.identity);
        PolygonCollider2D collider = asteroid.GetComponent<PolygonCollider2D>();

        if (collisionDetected(collider))
        {
            Destroy(asteroid);
            return false;
        }
        ((Asteroid)asteroid.GetComponent("Asteroid")).size = 0;
        colliders.Add(collider);
        return true;
    }

    private bool collisionDetected(PolygonCollider2D spawn)
    {
        foreach (PolygonCollider2D col in colliders)
        {
            if (col.bounds.Intersects(spawn.bounds))
                return true;
        }
        return false;
    }
}
