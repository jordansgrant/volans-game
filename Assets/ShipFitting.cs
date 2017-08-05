using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipFitting : MonoBehaviour {

    public Button solarSystem;

    //Load Player's current inventory

    void awake()
    {

    }

    void start()
    {
        print("HELLO");
        Button btn = solarSystem.GetComponent<Button>();
        print(btn);
        btn.onClick.AddListener(DoSelectSolarSystem);
        //loadInventory();
    }

    void DoSelectSolarSystem()
    {
        print("PRESSED");
        Debug.Log("SOLAR SYSTEM BTN");
        SceneManager.LoadScene("SolarSystem");
    }
}
