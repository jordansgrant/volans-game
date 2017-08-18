using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryAchieved : MonoBehaviour {

    public Button solarSystem,
                  fittingMenu;
    // Use this for initialization
    void Start()
    {

        Button btn = solarSystem.GetComponent<Button>();
        btn.onClick.AddListener(DoSolarSystem);

        btn = fittingMenu.GetComponent<Button>();
        btn.onClick.AddListener(DoFittingMenu);
    }

    void DoSolarSystem()
    {
        SceneManager.LoadScene(GameManager.game.sData.Level);
    }

    void DoFittingMenu()
    {
        SceneManager.LoadScene("FittingMenu");
    }
}
