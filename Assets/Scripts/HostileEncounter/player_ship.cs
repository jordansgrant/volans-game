using UnityEngine;

public class player_ship : MonoBehaviour
{
    public int armor;

    private float rotationSpeed;
    private int acceleration;
    private int maxVelocity;

    public GameObject projectile;
    private GameObject turret;
    private Rigidbody2D player;

    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        armor = 300;

        turret = GameObject.Find("turret");
        acceleration = 1000;
        maxVelocity = 600;
        rotationSpeed = 200;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        bool isVerticalPressed = Input.GetButton("Vertical");

        transform.Rotate(new Vector3(0, 0, horizontal * Time.deltaTime * rotationSpeed));

        if (isVerticalPressed)
        {
            float accelCoefficient = 1.0f - (player.velocity.magnitude / maxVelocity);
            player.AddForce(transform.up * Time.deltaTime * acceleration * accelCoefficient);
        }

        if (Input.GetKeyDown("space"))
        {
            Quaternion rotation = Quaternion.FromToRotation(projectile.transform.up, turret.transform.up);
            GameObject proj = Instantiate(projectile, turret.transform.position, rotation);
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
