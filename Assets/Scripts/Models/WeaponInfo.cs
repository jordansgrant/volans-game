using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponInfo  {

    public string projectile;
    public int fireCount;
    public float fireDelay;
    public float fireRate;

    public WeaponInfo()
    {
        projectile = "";
        fireCount = 0;
        fireDelay = 0f;
        fireRate = 0f;
    }

    public WeaponInfo(string weaponName, int count, float delay, float rate)
    {
        projectile = weaponName;
        fireCount = count;
        fireDelay = delay;
        fireRate = rate;
    }
}
