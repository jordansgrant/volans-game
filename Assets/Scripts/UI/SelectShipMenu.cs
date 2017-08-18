using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SelectShipMenu : MonoBehaviour
{

    public Button mainMenu,
                  startGame,
                  selectFrigate,
                  selectCruiser,
                  selectBattleship;

    public GameObject notificationPanel;
    public Text notification;

    private bool isSelectionMade = false;


    // Use this for initialization
    void Start()
    {
        Button btn = mainMenu.GetComponent<Button>();
        btn.onClick.AddListener(DoSelectMainMenu);

        btn = startGame.GetComponent<Button>();
        btn.onClick.AddListener(DoSelectStartGame);

        btn = selectFrigate.GetComponent<Button>();
        btn.onClick.AddListener(DoSelectFrigate);

        btn = selectCruiser.GetComponent<Button>();
        btn.onClick.AddListener(DoSelectCruiser);

        btn = selectBattleship.GetComponent<Button>();
        btn.onClick.AddListener(DoSelectBattleship);


    }

    void DisplayNotification(string text)
    {
        notification.text = text;
        notificationPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DoSelectFrigate()
    {
        string ship_type = "fighter";
        SaveShipType(ship_type);
        AdjustShipQualities(300, 200);
        isSelectionMade = true;

        DisplayNotification("Fighter Selected!");
    }

    void DoSelectCruiser()
    {
        string ship_type = "cruiser";
        SaveShipType(ship_type);
        AdjustShipQualities(500, 400);
        isSelectionMade = true;

        DisplayNotification("Cruiser Selected!");
    }

    void DoSelectBattleship()
    {
        string ship_type = "battleship";
        SaveShipType(ship_type);
        AdjustShipQualities(400, 300);
        isSelectionMade = true;

        DisplayNotification("Battleship Selected!");
    }

    void DoSelectMainMenu()
    {
        Debug.Log("Main menu!");
        LoadScene(0);
    }

    void DoSelectStartGame()
    {
        if (isSelectionMade == true)
        {
            //test
            GameManager.game.pData.moduleAttached.Add("BulletMod");
            GameManager.game.sData.Level = "SolarSystem1";
            SceneManager.LoadScene("SolarSystem1");
        }
        else
        {
            DisplayNotification("Please select a ship before continuing!");
        }
    }

    public void LoadScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }

    // Set ship type in game manager.
    void SaveShipType(string ship)
    {
        GameManager.game.pData.shipType = ship;
    }

    private void AdjustShipQualities(int armor, int power)
    {
        GameManager.game.pData.maxArmor = armor;
        GameManager.game.pData.maxPower = power;
    }
}
