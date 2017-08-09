using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SolarSystemGUI : MonoBehaviour {

    public Button mainMenu,
                  shipFitting;

    public GameObject notificationPanel;
    public Text notification;

    public static SolarSystemGUI instance = null;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //if (instance == null)
        //    instance = this;

        ////If instance already exists and it's not this:
        //else if (instance != this) 
        //    Destroy(gameObject);

        ////Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Button btn = mainMenu.GetComponent<Button>();
        btn.onClick.AddListener(DoSelectMainMenu);

        btn = shipFitting.GetComponent<Button>();
        btn.onClick.AddListener(DoShipInventory);

    }

    void DoSelectMainMenu()
    {
        Debug.Log("Main menu!");
        SceneManager.LoadScene("MainMenu");
    }

    void DoShipInventory()
    {
        SceneManager.LoadScene("FittingMenu");
    }

    public void DisplayNotification(string str)
    {
        notification.text = str;
    }
}
