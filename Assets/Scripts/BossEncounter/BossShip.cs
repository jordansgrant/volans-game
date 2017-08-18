using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossShip : MonoBehaviour
{

    public static int armor;

    public WeaponInfo laser;
    public WeaponInfo bullet;

    private Transform player;

    public GameObject laser_obj;
    public GameObject bullet_obj;

    private float main_lastFire = 0.0f;
    private float left_lastFire = 0.0f;
    private float right_lastFire = 0.0f;

    private GameObject main_turret;
    private GameObject left_turret;
    private GameObject right_turret;

    private bool isGameOver = false;

    void Awake()
    {
        armor = 1000;

        main_turret = GameObject.Find("enemy_turret_main");
        left_turret = GameObject.Find("enemy_turret_left");
        right_turret = GameObject.Find("enemy_turret_right");

        laser_obj.GetComponent<CollideType>().damage = laser.damage;
        bullet_obj.GetComponent<CollideType>().damage = bullet.damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            GameObject oPlayer = GameObject.FindGameObjectWithTag("Player");

            if (oPlayer == null)
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

        RaycastHit2D hit = Physics2D.Raycast(main_turret.transform.position, main_turret.transform.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("Foreground"));
        if (hit)
        {
            CollideType type = ((CollideType)hit.collider.gameObject.GetComponent("CollideType"));
            if (type.type == "ship" && Time.time > main_lastFire + laser.fireRate)
            {
                Fire(laser_obj, main_turret, laser.fireCount);
                main_lastFire = Time.time;
            }
        }

        hit = Physics2D.Raycast(left_turret.transform.position, left_turret.transform.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("Foreground"));
        if (hit)
        {
            CollideType type = ((CollideType)hit.collider.gameObject.GetComponent("CollideType"));
            if (type.type == "ship" && Time.time > left_lastFire + bullet.fireRate)
            {
                Fire(bullet_obj, left_turret, bullet.fireCount);
                left_lastFire = Time.time;
            }
        }

        hit = Physics2D.Raycast(right_turret.transform.position, right_turret.transform.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("Foreground"));
        if (hit)
        {
            CollideType type = ((CollideType)hit.collider.gameObject.GetComponent("CollideType"));
            if (type.type == "ship" && Time.time > right_lastFire + bullet.fireRate)
            {
                Fire(bullet_obj, right_turret, bullet.fireCount);
                left_lastFire = Time.time;
            }
        }

        checkForDead();

    }

    void Fire(GameObject projectile, GameObject turret, int count)
    {
        Quaternion rotation = Quaternion.FromToRotation(projectile.transform.up, turret.transform.up);
        Instantiate(projectile, turret.transform.position, rotation);

        if (count - 1 > 0)
            StartCoroutine(FireDelayed(projectile, turret, count - 1));
    }

    IEnumerator FireDelayed(GameObject projectile, GameObject turret, int count)
    {
        Quaternion rotation = Quaternion.FromToRotation(projectile.transform.up, turret.transform.up);
        for (int i = 0; i < count - 1; i++)
        {
            yield return new WaitForSeconds(bullet.fireDelay);
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
}
