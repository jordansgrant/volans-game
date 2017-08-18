using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyFleet : MonoBehaviour {

    public void toggleJustHadFleetEncounter()
    {
        bool fleetEncounter = GameManager.game.pData.JustHadFleetEncounter;

        fleetEncounter = (fleetEncounter == true) ? false : true;

        GameManager.game.pData.JustHadFleetEncounter = fleetEncounter;
    }

    private void LoadFleetEncounter(Collider2D ship)
    {
        GameManager.game.sData.playerPosition = ship.transform.position;
        SceneManager.LoadScene("HostileEncounter");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print(GameManager.game.pData.JustHadFleetEncounter);
        //toggleJustHadFleetEncounter();

        if (other.gameObject.tag == "Player" && GameManager.game.pData.JustHadFleetEncounter == false)
        {
            LoadFleetEncounter(other);
        }
    }
}
