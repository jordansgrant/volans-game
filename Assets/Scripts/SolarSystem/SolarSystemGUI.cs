﻿using System.Collections;
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
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this) 
            Destroy(gameObject);
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
        GameManager.game.Save();
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
