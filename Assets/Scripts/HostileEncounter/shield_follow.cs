using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield_follow : MonoBehaviour {

    public GameObject toFollow;

    void Start()
    {
        toFollow = PlayerShip.playerRef;

        Debug.Log(toFollow);
    }
    // Update is called once per frame
    void Update () {
        if (toFollow == null)
        {
            toFollow = PlayerShip.playerRef;
        }
        else
        {
            transform.position = toFollow.transform.position;
        }

	}
}
