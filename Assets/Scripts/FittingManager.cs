using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FittingManager : MonoBehaviour
{

    public Button solarSys;
    //Load Player's current inventory
    public void loadInventory()
    {
        //print(inventory.items[0].itemName);
    }

    void Awake()
    {
        Button btn = solarSys.GetComponent<Button>();
        btn.onClick.AddListener(BackToSolar);

        //Load Inventory
        print(GameObject.Find("ItemSlot0"));
        print(GameObject.Find("ArmorMod"));
        //GameManager.game.pData.moduleInventory.Add();

        foreach (var mod in GameManager.game.pData.moduleInventory)
        {
            //print(mod.getName());
        }
    }

    void start()
    {
        //loadInventory();
    }

    void BackToSolar()
    {
        Debug.Log("Back to Solar System!");
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
