using System.Collections;
using UnityEngine;

public class enemy_ship : MonoBehaviour
{
    public static int armor;

    public WeaponInfo weapon;
    private float lastFire = 0.0f;

    private Transform player;

    public GameObject projectile;
    private GameObject turret;
    private SpriteRenderer thruster;

    private bool isGameOver = false;

    // Use this for initialization
    void Start()
    {
        thruster.enabled = true;

        weapon.projectile = "laser_bolt";
        weapon.fireRate = 0.30f;
        weapon.fireDelay = 0.05f;
        weapon.fireCount = 1;
    }

    void Awake()
    {
        armor = 300;
        
        turret = GameObject.Find("enemy_turret");
        thruster = GameObject.Find("enemy_thruster").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            GameObject oPlayer = GameObject.FindGameObjectWithTag("Player");

            if (oPlayer == null )
            {
                if (!isGameOver)
                {
                    GameObject gameOver = Resources.Load("UI/Game Over") as GameObject;
                    Instantiate(gameOver, new Vector2(0, 2.5f), Quaternion.identity);
                    isGameOver = true;
                }
                return;
            }

            player = oPlayer.transform;
        }

        RaycastHit2D hit = Physics2D.Raycast(turret.transform.position, turret.transform.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("Foreground"));
        if (hit)
        {
            collide_type type = ((collide_type)hit.collider.gameObject.GetComponent("collide_type"));
            if (type.type == "ship" && Time.time > lastFire + weapon.fireRate)
            {
                //Quaternion rotation = Quaternion.FromToRotation(projectile.transform.up, turret.transform.up);
                //GameObject proj = Instantiate(projectile, turret.transform.position, rotation);
                Fire(weapon.fireCount);
                lastFire = Time.time;
            }
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
            yield return new WaitForSeconds(weapon.fireDelay);
            Instantiate(projectile, turret.transform.position, rotation);
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
            GameObject gameOver = Resources.Load("UI/Game Over Win") as GameObject;
            Instantiate(gameOver, new Vector2(0, 2.5f), Quaternion.identity);
            isGameOver = true;
            Destroy(gameObject);
        }
    }

}
