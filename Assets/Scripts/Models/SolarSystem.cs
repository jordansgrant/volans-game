using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SolarSystem
{

    public Dictionary<string, GameObject> Planets;

    public SolarSystem()
    {
        Planets = new Dictionary<string, GameObject>();
    }
}