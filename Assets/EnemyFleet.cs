using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyFleet : MonoBehaviour {

    private void LoadFleetEncounter(Collider2D ship)
    {
        GameManager.game.sData.playerPosition = ship.transform.position;
        SceneManager.LoadScene("HostileEncounter");
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    print(GameManager.game.pData.AllowOtherActions);
    //    print(gameObject.GetComponent<BoxCollider2D>().bounds.Intersects(other.bounds));
    //    if (other.gameObject.tag == "Player" && GameManager.game.pData.AllowOtherActions == true)
    //    {
    //        LoadFleetEncounter(other);
    //    }
    //}

    void OnTriggerStay2D(Collider2D other)
    {
        //print(GameManager.game.pData.AllowOtherActions);
        if (other.gameObject.tag == "Player" && GameManager.game.pData.AllowOtherActions == true)
        {
            LoadFleetEncounter(other);
        }
    }
}
