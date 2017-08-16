using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int      maxArmor;
    public int      maxPower;

    public float    rotationSpeed;
    public int      acceleration;
    public int      maxVelocity;

    public WeaponInfo weapon;

    public string   shipType;

    public int      powerRechargeSize;
    public float    powerRechargeRate;

    public List<string> moduleInventory;
    public List<string> moduleAttached;

    public bool isTestDataLoaded = false;

    public PlayerData()
    {
        moduleInventory = new List<string>();
        moduleAttached = new List<string>();

    }

    //I was not sure whether to implement a wrapper class
    //for player inventory.
    public Dictionary<string, GameObject> playerInventory;
}
