using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FittingManager : MonoBehaviour
{

    public Button SolarSys;
    public Image Ship;

    private Sprite shipImg;

    private void LoadShip()
    {
        string shipType = GameManager.game.pData.shipType;
        shipImg = Resources.Load<Sprite>(@"Ships\ShipSprites\" + shipType) as Sprite;
    }

    private void DrawShip()
    {
        //print(Ship);
        Ship.GetComponent<Image>().sprite = shipImg;
    }

    //Load Player's current inventory
    public void LoadInventory()
    {

        //print(inventory.items[0].itemName);
    }

    void Awake()
    {
        Button btn = SolarSys.GetComponent<Button>();
        btn.onClick.AddListener(BackToSolar);

        //Load Inventory
        print(GameObject.Find("ItemSlot0"));

        //Load current ship sprite
        LoadShip();

        //GameManager.game.pData.moduleInventory.Add();

        foreach (var mod in GameManager.game.pData.moduleInventory)
        {
            //print(mod.getName());
        }
    }

    void Start()
    {
        DrawShip();
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
