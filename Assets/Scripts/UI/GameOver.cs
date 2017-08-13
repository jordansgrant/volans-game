using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

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
        SceneManager.LoadScene(0);
    }

    void DoExitGame()
    {
        Application.Quit();
    }
}
