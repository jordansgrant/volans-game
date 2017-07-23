using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travelable : MonoBehaviour {

    public List<string> Connections;


    //public Vector2 Position;
    public string Type;
    public string Name;

    bool IsPlayerHere
    {
        set { IsPlayerHere = value; }
        get { return IsPlayerHere; }
    }
    int Difficulty
    {
        set { Difficulty = value; }
        get { return Difficulty; }
    }
    bool WasVisited
    {
        set { WasVisited = value; }
        get { return WasVisited;  }
    }

    public void Initialize(string Name, List<string> Connections,
        int Difficulty, bool IsPlayerHere)
    {
        this.Name = Name;
        this.WasVisited = false;
        this.Difficulty = Difficulty;
        this.IsPlayerHere = IsPlayerHere;
    }
    
}
