using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travelable : MonoBehaviour {

    public List<int> Connections;


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

    public void Initialize(string Name, Vector2 Position,
         List<int> Connections, int Difficulty)
    {
        this.Name = Name;
        this.WasVisited = false;
        this.Difficulty = Difficulty;
    }
    
}
