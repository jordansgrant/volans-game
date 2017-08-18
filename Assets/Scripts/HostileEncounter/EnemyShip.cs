using System.Collections;
using UnityEngine;

public class EnemyShip : MonoBehaviour
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
    }

    void Awake()
    {
        armor = 300;
        
        turret = GameObject.Find("enemy_turret");
        thruster = GameObject.Find("enemy_thruster").GetComponent<SpriteRenderer>();

        weapon = GetEnemyWeapon(1);
        projectile = Resources.Load(weapon.projectile) as GameObject;
        projectile.GetComponent<CollideType>().damage = weapon.damage;
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
            CollideType type = ((CollideType)hit.collider.gameObject.GetComponent("CollideType"));
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
            GameObject playerVictory = Resources.Load("UI/Victory Achieved") as GameObject;
            Instantiate(playerVictory, new Vector2(0, 2.5f), Quaternion.identity);
            isGameOver = true;
            Destroy(gameObject);
        }
    }

    public WeaponInfo GetEnemyWeapon(int solarSystem)
    {
        System.Random rnd = new System.Random(System.DateTime.Now.Millisecond);
        int weaponIndex = rnd.Next(0,2) ; //between 0 and 1

        Debug.Log(weaponIndex);

        WeaponInfo weapon = null;

        switch(weaponIndex)
        {
            case 0:
                weapon = GameManager.game.weaponTypes["laser_bolt"];
                weapon.damage = GetEnemyStats(solarSystem, false);
                break;
            case 1:
                weapon = GameManager.game.weaponTypes["bullet"];
                weapon.damage = GetEnemyStats(solarSystem, false) / 2;
                break;
        }

        return weapon;
    }

    public int GetEnemyStats(int difficulty, bool isEmpire)
    {
        if (isEmpire)
            return 40;

        switch (difficulty)
        {
            case 1:
                armor = (isEmpire) ? 400 : 300;
                return (isEmpire) ? 30 : 20;
            case 2:
                armor = (isEmpire) ? 600 : 450;
                return (isEmpire) ? 35 : 25;
            case 3:
                armor = (isEmpire) ? 800 : 600;
                return (isEmpire) ? 45 : 30;
        }

        return 40;
    }

}
