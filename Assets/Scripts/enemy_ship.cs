using UnityEngine;

public class enemy_ship : MonoBehaviour
{
    public int armor;
    
    private float fireRate = 0.25f;
    private float lastFire = 0.0f;
    //private int acceleration;
    //private int maxVelocity;

    public GameObject projectile;
    private GameObject turret;
    private SpriteRenderer thruster;
    private Rigidbody2D self;

    private Transform player;

    // Use this for initialization
    void Start()
    {
        thruster.enabled = true;
    }

    void Awake()
    {
        self = GetComponent<Rigidbody2D>();
        armor = 300;
        
        turret = GameObject.Find("enemy_turret");
        thruster = GameObject.Find("enemy_thruster").GetComponent<SpriteRenderer>();
        //acceleration = 1000;
        //maxVelocity = 400;
        //rotationSpeed = 200;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        RaycastHit2D hit = Physics2D.Raycast(turret.transform.position, turret.transform.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("Foreground"));
        collide_type type = ((collide_type)hit.collider.gameObject.GetComponent("collide_type"));
        if (type.type == "ship" && Time.time > lastFire + fireRate)
        {
            Quaternion rotation = Quaternion.FromToRotation(projectile.transform.up, turret.transform.up);
            GameObject proj = Instantiate(projectile, turret.transform.position, rotation);
            lastFire = Time.time;
        }

    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        var type = collision.gameObject.GetComponent("collide_type") as collide_type;
        switch (type.type)
        {
            case "asteroid_large":
                armor -= type.damage;
                break;
            case "asteroid_med":
                armor -= type.damage;
                break;
            case "asteroid_small":
                armor -= type.damage;
                break;
            case "ship":
                armor -= type.damage;
                break;
        }
        Debug.Log(" Enemy Here at a collision");
        checkForDead();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var type = collision.gameObject.GetComponent("collide_type") as collide_type;
        
        switch (type.type)
        {
            case "projectile":
                armor -= type.damage;
                break;
        }

        checkForDead();
    }

    private void checkForDead()
    {
        if (armor < 0)
        {
            Destroy(gameObject);
        }
    }

}
