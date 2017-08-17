using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TitleMenu : MonoBehaviour {

    public Button newGame,
                  continueGame,
                  exitGame;

    // Use this for initialization
    void Start ()
    {
        try
        {
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
            file.Close();
            continueGame.interactable = true;
        }
        catch
        {
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
        LoadScene("ShipSelection");
    }

    void DoContinueGame()
    {
        Debug.Log("Continue Game!");
        
        GameObject manager = new GameObject("GameManager");
        manager.AddComponent<GameManager>();
        GameManager.game.Load();

        LoadScene("SolarSystem");
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
