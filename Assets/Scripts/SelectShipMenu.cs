using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectShipMenu : MonoBehaviour
{

    public Button mainMenu,
                  startGame,
                  selectFrigate,
                  selectCruiser,
                  selectBattleship;

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

    // Update is called once per frame
    void Update()
    {

    }

    void DoSelectFrigate()
    {
        Debug.Log("Selected Frigate!");
    }

    void DoSelectCruiser()
    {
        Debug.Log("Selected Cruiser!");
    }

    void DoSelectBattleship()
    {
        Debug.Log("Selected Battleship!");
    }

    void DoSelectMainMenu()
    {
        Debug.Log("Main menu!");
    }

    void DoSelectStartGame()
    {
        Debug.Log("Start Game!");
    }
}
