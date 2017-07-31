using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public GameObject itemObject;
    public string itemName;
    public int damage;
    public bool isShieldPiercing;
}
