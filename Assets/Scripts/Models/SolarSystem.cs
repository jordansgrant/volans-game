using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[System.Serializable]
public class SolarSystem
{
    public List<string> UsedNames;
  
    public Dictionary<string, GameObject> Planets;
   
    public Dictionary<string, PlanetData> PlanetsData;

    public bool isSpawned;
    public int Turn;
    
    public Vector2 playerPosition;

    public bool isStartingPosition = true;
    public bool isFleetEncounter = false;
    public bool isTest = true;
    public string Level;
    
    public Vector2 gatePosition;

    public bool IsGameStarted = false;

    public SolarSystem()
    {
        playerPosition = new Vector2();
        Turn = 0;
        isSpawned = false;
        UsedNames = new List<string>();
        Planets = new Dictionary<string, GameObject>();
        PlanetsData = new Dictionary<string, PlanetData>();
    }
}

[System.Serializable]
public class PlanetData
{
    public string Name { get; set; }
    public int PreFabNum { get; set; }

    public Vector2 Position { get; set; }

    public int Difficulty { get; set; }
    public string Tag { get; set; }
    public bool wasVisited { get; set; }
}

public class Vector2SerializationSurrogate : ISerializationSurrogate
{

    // Method called to serialize a Vector3 object
    public void GetObjectData(System.Object obj, SerializationInfo info, StreamingContext context)
    {

        Vector2 v2 = (Vector2)obj;
        info.AddValue("x", v2.x);
        info.AddValue("y", v2.y);
    }

    // Method called to deserialize a Vector3 object
    public System.Object SetObjectData(System.Object obj, SerializationInfo info,
                                       StreamingContext context, ISurrogateSelector selector)
    {

        Vector2 v2 = (Vector2)obj;
        v2.x = (float)info.GetValue("x", typeof(float));
        v2.y = (float)info.GetValue("y", typeof(float));
        obj = v2;
        return obj;
    }
}