using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SolarSystem
{

    public Dictionary<string, GameObject> Planets;
    public bool isSpawned;
    public int test;

    public SolarSystem()
    {
        test = 1;
        isSpawned = false;
        Planets = new Dictionary<string, GameObject>();
    }
}