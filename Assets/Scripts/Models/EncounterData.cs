using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EncounterData {

    public string       encounterType;
    public int          difficulty;
    public EnemyData    enemy;
    public IModule      reward;
    
}
