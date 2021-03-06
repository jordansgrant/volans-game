﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class TitleMenu : MonoBehaviour {

    public Button newGame,
                  continueGame,
                  exitGame;

    // Use this for initialization
    void Start ()
    {
        print("here");
        try
        {
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
            file.Close();
            continueGame.interactable = true;
        }
        catch (Exception ex)
        {
            print(ex);
            continueGame.interactable = false;
        }
    }

    void Awake()
    {
        Button btn = newGame.GetComponent<Button>();
        btn.onClick.AddListener(DoNewGame);

        btn = continueGame.GetComponent<Button>();
        btn.onClick.AddListener(DoContinueGame);

        btn = exitGame.GetComponent<Button>();
        btn.onClick.AddListener(DoExitGame);
        
    }

    void DoNewGame()
    {
        if (continueGame.interactable)
        {
            File.Delete(Application.persistentDataPath + "/player.dat");
            File.Delete(Application.persistentDataPath + "/solar_system.dat");
        }
        LoadScene("ShipSelection");
    }

    void DoContinueGame()
    {
        GameObject manager = new GameObject("GameManager");
        manager.AddComponent<GameManager>();
        GameManager.game.Load();

        LoadScene(GameManager.game.sData.Level);
    }

    void DoExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
