using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class GameOver : MonoBehaviour {
    public Button mainMenu,
                  exitGame;
    // Use this for initialization
    void Start () {

        Button btn = mainMenu.GetComponent<Button>();
        btn.onClick.AddListener(DoMainMenu);

        btn = exitGame.GetComponent<Button>();
        btn.onClick.AddListener(DoExitGame);
    }
	
	void DoMainMenu()
    {
        ResetGameManager();
        SceneManager.LoadScene(0);
    }

    void DoExitGame()
    {
        ResetGameManager();
        Application.Quit();
    }

    void ResetGameManager()
    {
        GameManager.game.sData.IsGameStarted = false;
        GameManager.game.pData.IsGameStarted = false;

        try
        {
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
            file.Close();
            File.Delete(Application.persistentDataPath + "/player.dat");
            File.Delete(Application.persistentDataPath + "/solar_system.dat");
        }
        catch  { }

    }
}
