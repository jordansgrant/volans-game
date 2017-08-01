using UnityEngine;

public class player_ship : MonoBehaviour
{
    public int maxArmor;
    public int maxPower;
    public int powerRechargeSize;

    public static int armor;
    public static int power;
    public static GameObject playerRef;

    private float rotationSpeed = 200;
    private int acceleration = 1000;
    private int maxVelocity = 600;

    private float powerRechargeRate = 0.4f;
    private float lastRechargeTick = 0.0f;

    public GameObject projectile;
    private GameObject turret;
    private Rigidbody2D player;
    private Shield shield;

   void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        armor = maxArmor;
        power = maxPower;

        turret = GameObject.Find("turret");

        playerRef = this.gameObject;

        shield = gameObject.GetComponentInChildren<Shield>();
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

        if (!Input.GetKey("c") &&
            Time.time > lastRechargeTick + powerRechargeRate 
            && power < maxPower)
        {
            if (power + powerRechargeSize > maxPower)
                power = maxPower;

            else
                power += powerRechargeSize;

            lastRechargeTick = Time.time;
        }

        checkForDead();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (shield.isEnabled)
            return;
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
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (shield.isEnabled)
            return;
        var type = collision.gameObject.GetComponent("collide_type") as collide_type;
        
        switch (type.type)
        {
            case "projectile":
                armor -= type.damage;
                break;
        }
        
    }

    private void checkForDead()
    {
        if (armor < 0)
        {
            Destroy(gameObject);
        }
    }


}
