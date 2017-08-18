using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponInfo  {

    public string projectile;
    public int fireCount;
    public float fireDelay;
    public float fireRate;
    public int damage;

    public WeaponInfo()
    {
        projectile = "";
        fireCount = 0;
        fireDelay = 0f;
        fireRate = 0f;
        damage = 0;
    }

    public WeaponInfo(string weaponName, int count, float delay, float rate, int weaponDamage)
    {
        projectile = weaponName;
        fireCount = count;
        fireDelay = delay;
        fireRate = rate;
        damage = weaponDamage;
    }
}
