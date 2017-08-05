using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ShipFittingManager : MonoBehaviour {
    
    public PlayerInventory inventory;
    public Button solarSystem;

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
        Button btn = solarSystem.GetComponent<Button>();
        btn.onClick.AddListener(DoSelectSolarSystem);

        loadInventory();
    }

    void DoSelectSolarSystem()
    {
        Debug.Log("SOLAR SYSTEM BTN");
        SceneManager.LoadScene("SolarSystem");
    }
    //Move item from inventory to ship
    //Remove item from inventory
    //Add to ship

    //Remove item from ship and into inventory
    //remove item from ship
    //add to inventory

    //Trash item from inventory
}
