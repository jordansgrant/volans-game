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

    private void LoadShipFit()
    {
        GameObject.Find("ModSlot0");
        int i = 0;
        string path;
        foreach (var mod in GameManager.game.pData.moduleAttached)
        {
            module = Resources.Load<GameObject>(@"Modules\" + mod) as GameObject;
            print(module);
            modules.Add(module);
            path = "ModSlot" + i + "/Button";

            GameObject.Find(path).GetComponent<Image>().sprite = module.GetComponentInChildren<Image>().sprite;
            GameObject.Find(path).GetComponent<Image>().enabled = true;

            i++;
        }
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
            path = "InventorySlot" + i + "/InvButton" + i;

            GameObject.Find(path).GetComponent<Image>().sprite = module.GetComponentInChildren<Image>().sprite;
            GameObject.Find(path).GetComponent<Image>().enabled = true;
            GameObject.Find(path).GetComponentInChildren<Text>().text = mod;

            i++;
        }
    }


    void InitializeInvButtons()
    {
        
        for(int i = 0; i < 6; i++)
        {
            string path = "InvButton" + i;
            print(path);
            Button btn = GameObject.Find(path).GetComponent<Button>();
            btn.onClick.AddListener(delegate { AddToFit(path); });
        }

    }

    void Awake()
    {
        //Solar system button
        Button btn = SolarSys.GetComponent<Button>();
        btn.onClick.AddListener(BackToSolar);

        //Module buttons
        InitializeInvButtons();

        modules = new List<GameObject>();

        //Test data
        GameManager.game.pData.moduleInventory.Add("ArmorMod");
        GameManager.game.pData.moduleInventory.Add("PowerMod");
        GameManager.game.pData.moduleAttached.Add("PowerMod");

        //Load Inventory
        LoadInventory();

        //Load Fitted modules
        LoadShipFit();

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

    void AddToFit(string current)
    {

        GameObject.Find(current).GetComponent<Image>().enabled = false;
        string module = GameObject.Find(current).GetComponentInChildren<Text>().text;
        GameObject.Find(current).GetComponentInChildren<Text>().text = "";
        GameManager.game.pData.moduleInventory.Add(module);

        DrawAddedToFitting(module);

        print(GameObject.Find(current).GetComponent<Image>());

    }

    bool DrawAddedToFitting(string module)
    {
        print(GameObject.Find("Modules"));
        GameObject newMod = Resources.Load<GameObject>(@"Modules\" + module) as GameObject;
        
        //4 should eventually be replaced by number of slots on a ship
        for(int i = 0; i < 4; i++)
        {
            if(GameObject.Find("ModSlot" + i).GetComponentInChildren<Button>().GetComponent<Image>().sprite.name == "UISprite")
            {
                //print(GameObject.Find("ModSlot" + i).GetComponentInChildren<Button>().GetComponent<Image>().sprite);
                GameObject.Find("ModSlot" + i).GetComponentInChildren<Button>().GetComponent<Image>().sprite = newMod.GetComponentInChildren<Image>().sprite;
                GameObject.Find("ModSlot" + i).GetComponentInChildren<Button>().GetComponent<Image>().enabled = true;
                return true;
            }
        }

        //False if no space to add object
        return false;
    }
    
    //Move item from inventory to ship
    //Remove item from inventory
    //Add to ship

    //Remove item from ship and into inventory
    //remove item from ship
    //add to inventory

    //Trash item from inventory
}
