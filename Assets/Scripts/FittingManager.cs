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

    private int ShipSlots;
    private int InventorySlots;
    private string ship_type;

    private int currFittedMods = 0;
    private int currInvMods = 0;

    private void LoadShip()
    {
        string shipType = GameManager.game.pData.shipType;
        shipImg = Resources.Load<Sprite>(@"Ships\ShipSprites\" + shipType) as Sprite;
    }

    private void DrawShip()
    {
        Ship.GetComponent<Image>().sprite = shipImg;
    }

    void SetupSlots()
    {

        ship_type = GameManager.game.pData.shipType;
        switch (ship_type)
        {
            case "fighter":
                ShipSlots = 2;
                InventorySlots = 3;
                break;
            case "cruiser":
                ShipSlots = 3;
                InventorySlots = 4;
                break;
            case "battleship":
                ShipSlots = 4;
                InventorySlots = 5;
                break;
            default:
                ShipSlots = 2;
                InventorySlots = 2;
                break;
        }
    }


    private void LoadInventory()
    {
        int i = 0;
        string path;
        ClearInventory();
        print("After loading inventory");
        //print(GameManager.game.pData.moduleAttached.Count);
        //print(GameManager.game.pData.moduleInventory.Count);
        //currInvMods = 0;
        foreach (var mod in GameManager.game.pData.moduleInventory)
        {
            module = Resources.Load<GameObject>(@"Modules\" + mod) as GameObject;
            //print(module);
            //modules.Add(module);
            path = "InventorySlot" + i + "/InvButton" + i;

            GameObject.Find(path).GetComponent<Image>().sprite = module.GetComponentInChildren<Image>().sprite;
            GameObject.Find(path).GetComponent<Image>().enabled = true;
            GameObject.Find(path).GetComponentInChildren<Text>().text = mod;

            i++;
            currInvMods++;
        }
        print("mods in inventory after load: " + currInvMods);
    }

    private void LoadShipFit()
    {
        int i = 0;
        string path;
        //ClearFit();
        print("After loading ship fit");
        //print(GameManager.game.pData.moduleAttached.Count);
        //print(GameManager.game.pData.moduleInventory.Count);
        //currInvMods = 0;
        foreach (var mod in GameManager.game.pData.moduleAttached)
        {
            path = "ModSlot" + i + "/Button";
            module = Resources.Load<GameObject>(@"Modules\" + mod) as GameObject;
            //print(module);
            //modules.Add(module);

            GameObject.Find(path).GetComponent<Image>().sprite = module.GetComponentInChildren<Image>().sprite;
            GameObject.Find(path).GetComponent<Image>().enabled = true;
            GameObject.Find(path).GetComponentInChildren<Text>().text = mod;

            i++;
            currFittedMods++;
        }

        print("mods fitted after load: " + currFittedMods);
    }

    void ClearInventory()
    {
        string path;
        for (int i = 0; i < InventorySlots; i++)
        {
            path = "InventorySlot" + i + "/InvButton" + i;
            //print(path);
            GameObject.Find(path).GetComponent<Image>().enabled = false;
            GameObject.Find(path).GetComponentInChildren<Text>().text = "";
        }
    }

    void ClearFit()
    {
        string path;
        for (int i = 0; i < ShipSlots; i++)
        {
            path = "ModSlot" + i;
            GameObject.Find(path).GetComponent<Image>().enabled = false;
            GameObject.Find(path).GetComponentInChildren<Text>().text = "";
        }
    }

    void InitializeInvButtons()
    {
        int j = 0;
        for(int i = 0; i < InventorySlots; i++)
        {
            string path = "InvButton" + i;
            print(path);
            Button btn = GameObject.Find(path).GetComponent<Button>();
            btn.onClick.AddListener(delegate { AddToFit(path); });
            j = i + 1;
        }

        while(j < 5)
        {
            string path = "InventorySlot" + j +"BackgroundImage";
            GameObject.Find(path).GetComponent<Image>().enabled = false;
            j++;
        }

    }

    void InitializeFitButtons()
    {

        int j = 0;
        for (int i = 0; i < ShipSlots; i++)
        {
            string path = "ModSlot" + i;
            //print(path);
            Button btn = GameObject.Find(path).GetComponentInChildren<Button>();
            //print(btn);
            btn.onClick.AddListener(delegate { AddToInventory(path); });
            j = i + 1;
        }

        while (j < 4)
        {
            string path = "ModSlot" + j + "BackgroundImage";
            GameObject.Find(path).GetComponent<Image>().enabled = false;
            j++;
        }
    } 

    void Awake()
    {
        //Solar system button
        Button btn = SolarSys.GetComponent<Button>();
        btn.onClick.AddListener(BackToSolar);

        SetupSlots();

        //Module buttons
        InitializeInvButtons();
        InitializeFitButtons();

        modules = new List<GameObject>();

        //Test data
        if(GameManager.game.pData.isTestDataLoaded == false)
        {
            GameManager.game.pData.moduleInventory.Add("ArmorMod");
            GameManager.game.pData.moduleInventory.Add("PowerMod");
            GameManager.game.pData.moduleAttached.Add("PowerMod");
            GameManager.game.pData.isTestDataLoaded = true;
        }

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
        //ClearFit();
        print("Before back to solar");

        ClearInventory();
        SceneManager.LoadScene(GameManager.game.sData.Level);
    }


    bool DrawAddedToFitting(string module)
    {
        print(GameObject.Find("Modules"));
        GameObject newMod = Resources.Load<GameObject>(@"Modules\" + module) as GameObject;
        
        //4 should eventually be replaced by number of slots on a ship
        for(int i = 0; i < ShipSlots; i++)
        {
            if(GameObject.Find("ModSlot" + i).GetComponentInChildren<Button>().GetComponent<Image>().sprite.name == "UISprite")
            {
                //print(GameObject.Find("ModSlot" + i).GetComponentInChildren<Button>().GetComponent<Image>().sprite);
                GameObject.Find("ModSlot" + i).GetComponentInChildren<Button>().GetComponent<Image>().sprite = newMod.GetComponentInChildren<Image>().sprite;
                GameObject.Find("ModSlot" + i).GetComponentInChildren<Button>().GetComponent<Image>().enabled = true;
                GameObject.Find("ModSlot" + i).GetComponentInChildren<Text>().text = module;
                return true;
            }
        }

        //False if no space to add object
        return false;
    }
    

    void AddToInventory(string current)
    {
        print("inv: " + currInvMods);
        print("fitted: " + currFittedMods);
        if (currInvMods >= InventorySlots)
        {
            return;
        }
        else
        {
            //currInvMods++;
            //currFittedMods--;
        }

        GameObject.Find(current).GetComponentInChildren<Button>().GetComponentInChildren<Image>().enabled = false;
        string module = GameObject.Find(current).GetComponentInChildren<Text>().text;

        GameObject.Find(current).GetComponentInChildren<Text>().text = "";

        print(module);
        GameManager.game.pData.moduleInventory.Add(module);
        GameManager.game.pData.moduleAttached.Remove(module);


        LoadInventory();
    }

    void AddToFit(string current)
    {

        print("inv: " + currInvMods);
        print("fitted: " + currFittedMods);
        if (currFittedMods >= ShipSlots)
        {
            return;
        }
        else
        {
            //currInvMods--;
            //currFittedMods++;
        }
        print("after add to fit in inv: " + currInvMods);
        print("after add to fit in fitted: " + currFittedMods);

        //GameObject.Find(current).GetComponent<Image>().enabled = false;
        string module = GameObject.Find(current).GetComponentInChildren<Text>().text;
        //GameObject.Find(current).GetComponentInChildren<Text>().text = "";
        GameManager.game.pData.moduleAttached.Add(module);
        GameManager.game.pData.moduleInventory.Remove(module);

        LoadShipFit();
        LoadInventory();
        //DrawAddedToFitting(module);

        //print(GameObject.Find(current).GetComponent<Image>());
    }

    void AddModuleEffect(string mod)
    {
        GameObject module = Resources.Load("Modules/" + mod) as GameObject;
        module.GetComponent<IModule>().addEffect();
    }

    void RemoveModuleEffect(string mod)
    {
        GameObject module = Resources.Load("Modules/" + mod) as GameObject;
        module.GetComponent<IModule>().removeEffect();
    }

    //Trash item from inventory
}
