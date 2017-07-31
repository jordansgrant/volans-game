using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipFittingManager : MonoBehaviour {
    
    public PlayerInventory inventory;
    public Button SolarSystem;

    //Load Player's current inventory
    public void loadInventory()
    {
        print(inventory.items[0].itemName);
    }

    void awake()
    {
        inventory = gameObject.GetComponents<PlayerInventory>()[0];
    }

    void start()
    {
        print("Hello");
        loadInventory();
    }
    //Move item from inventory to ship
        //Remove item from inventory
        //Add to ship
    
    //Remove item from ship and into inventory
        //remove item from ship
        //add to inventory
    
    //Trash item from inventory
}
