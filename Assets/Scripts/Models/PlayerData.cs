﻿using UnityEngine;
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

    public string   projectile;
    public string   shipType;

    public int      powerRechargeSize;
    public float    powerRechargeRate;

    public List<IModule> moduleInventory;
    public List<IModule> moduleAttached;


    public PlayerData()
    {
        moduleInventory = new List<IModule>();
        moduleAttached = new List<IModule>();
    }

    //I was not sure whether to implement a wrapper class
    //for player inventory.
    public Dictionary<string, GameObject> playerInventory;
}
