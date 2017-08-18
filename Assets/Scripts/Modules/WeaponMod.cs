using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponMod : MonoBehaviour, IModule
{
    
    public int fireCount;
    public float fireRate;
    public float fireDelay;
    public string projectile;
    public int damage;
    
    private bool active = false;

    public string name;

    public string getName()
    {
        return name;
    }

    public bool isActive()
    {
        return active;
    }

    public void addEffect()
    {
        GameManager.game.pData.weapon = new WeaponInfo(projectile, fireCount, fireDelay, fireRate, damage);
        active = true;
    }

    public void removeEffect()
    {
        GameManager.game.pData.weapon = null;
        active = false;
    }
}
