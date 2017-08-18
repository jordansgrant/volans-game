using System.Collections;
using System.Threading;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public static int armor;
    public static int power;
    public static GameObject playerRef;
    
    private float lastRechargeTick = 0.0f;
    private float lastFired = 0.0f;

    private PlayerData pData;

    public GameObject projectile;
    private GameObject turret;
    private Rigidbody2D player;
    private Shield shield;

   void Start()
    {
        player = GetComponent<Rigidbody2D>();

        pData = GameManager.game.pData;
        armor = pData.maxArmor;
        power = pData.maxPower;

        projectile = Resources.Load(pData.weapon.projectile) as GameObject;
        print(projectile);
        turret = GameObject.Find("turret");

        projectile.GetComponent<CollideType>().damage = pData.weapon.damage;

        playerRef = this.gameObject;

        shield = gameObject.GetComponentInChildren<Shield>();

        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * -1;
        bool isVerticalPressed = Input.GetButton("Vertical");

        transform.Rotate(new Vector3(0, 0, horizontal * Time.deltaTime * pData.rotationSpeed));

        if (isVerticalPressed)
        {
            float accelCoefficient = 1.0f - (player.velocity.magnitude / pData.maxVelocity);
            player.AddForce(transform.up * Time.deltaTime * pData.acceleration * accelCoefficient);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) &&
            pData.weapon != null &&
            Time.time > lastFired + pData.weapon.fireRate)
        {
            lastFired = Time.time;
            Fire(pData.weapon.fireCount);
        }

        if (!Input.GetKey("c") &&
            Time.time > lastRechargeTick + pData.powerRechargeRate 
            && power < pData.maxPower)
        {
            if (power + pData.powerRechargeSize > pData.maxPower)
                power = pData.maxPower;

            else
                power += pData.powerRechargeSize;

            lastRechargeTick = Time.time;
        }

        checkForDead();
    }

    void Fire(int count)
    {
        Quaternion rotation = Quaternion.FromToRotation(projectile.transform.up, turret.transform.up);
        Instantiate(projectile, turret.transform.position, rotation);

        if (count - 1 > 0) 
            StartCoroutine(FireDelayed(count - 1));
    }

    IEnumerator FireDelayed(int count)
    {
        Quaternion rotation = Quaternion.FromToRotation(projectile.transform.up, turret.transform.up);
        for (int i = 0; i < count - 1; i++)
        {
            yield return new WaitForSeconds(pData.weapon.fireDelay);
            Instantiate(projectile, turret.transform.position, rotation);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (shield.isEnabled)
            return;
        CollideType type = collision.gameObject.GetComponent("CollideType") as CollideType;
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
            case "shield":
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
        CollideType type = collision.gameObject.GetComponent("CollideType") as CollideType;
        
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
