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
    private List<GameObject> modules;
    private GameObject module;

    private void LoadShip()
    {
        string shipType = GameManager.game.pData.shipType;
        shipImg = Resources.Load<Sprite>(@"Ships\ShipSprites\" + shipType) as Sprite;
    }

    private void DrawShip()
    {
        Ship.GetComponent<Image>().sprite = shipImg;
    }

    private void LoadInventory()
    {
        GameObject.Find("ItemSlot0");
        int i = 0;
        string path;
        foreach (var mod in GameManager.game.pData.moduleInventory)
        {
            module = Resources.Load<GameObject>(@"Modules\" + mod) as GameObject;
            print(module);
            modules.Add(module);
            path = "InventorySlot" + i + "/ItemImage";

            GameObject.Find(path).GetComponent<Image>().sprite = module.GetComponentInChildren<Image>().sprite;
            GameObject.Find(path).GetComponent<Image>().enabled = true;

            i++;
        }
    }

    void Awake()
    {
        Button btn = SolarSys.GetComponent<Button>();
        btn.onClick.AddListener(BackToSolar);

        modules = new List<GameObject>();

        //Test data
        GameManager.game.pData.moduleInventory.Add("ArmorMod");
        GameManager.game.pData.moduleInventory.Add("PowerMod");

        //Load Inventory
        LoadInventory();

        //Load current ship sprite
        LoadShip();


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
