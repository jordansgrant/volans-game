using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SolarSystem
{

    public struct PlanetData
    {
        public string Name { get; set; }
        public int PreFabNum { get; set; }
        public Vector2 Position { get; set; }

        public int Difficulty { get; set; }
        public string Tag { get; set; }
        public bool wasVisited { get; set; }

        //Module Reward
        //Encounter type
    }

    public List<string> UsedNames;
    public Dictionary<string, GameObject> Planets;
    public Dictionary<string, PlanetData> PlanetsData;

    public bool isSpawned;

    public SolarSystem()
    {
        isSpawned = false;
        UsedNames = new List<string>();
        Planets = new Dictionary<string, GameObject>();
        PlanetsData = new Dictionary<string, PlanetData>();
    }
}