using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {

    public Button newGame,
                  continueGame,
                  exitGame;

    // Use this for initialization
    void Start ()
    {
        Button btn = newGame.GetComponent<Button>();
        btn.onClick.AddListener(DoNewGame);

        btn = continueGame.GetComponent<Button>();
        btn.onClick.AddListener(DoContinueGame);

        btn = exitGame.GetComponent<Button>();
        btn.onClick.AddListener(DoExitGame);
    }
    
    // Update is called once per frame
    void Update ()
    {
        
    }

    void DoNewGame()
    {
        Debug.Log("New Game!");
        LoadScene(1);
    }

    void DoContinueGame()
    {
        Debug.Log("Continue Game!");
    }

    void DoExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }
}
