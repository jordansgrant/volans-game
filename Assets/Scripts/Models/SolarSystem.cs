using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SolarSystem
{

    public class PlanetData
    {
        public string Name { get; set; }
        public int PreFabNum { get; set; }
        public Vector2 Position { get; set; }

        public int Difficulty { get; set; }
        public string Tag { get; set; }
        public bool wasVisited { get; set; }
    }

    public List<string> UsedNames;
    public Dictionary<string, GameObject> Planets;
    public Dictionary<string, PlanetData> PlanetsData;

    public bool isSpawned;
    public int Turn;

    public Vector2 playerPosition;
    public bool isStartingPosition = true;
    public bool isFleetEncounter = false;
    public bool isTest = true;
    public int Level;
    public Vector2 gatePosition;

    public SolarSystem()
    {
        playerPosition = new Vector2();
        Turn = 0;
        Level = 0;
        isSpawned = false;
        UsedNames = new List<string>();
        Planets = new Dictionary<string, GameObject>();
        PlanetsData = new Dictionary<string, PlanetData>();
    }
}