using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thruster : MonoBehaviour {
    public SpriteRenderer thrust;
    // Use this for initialization
    void Start () {
        thrust = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        bool isVerticalPressed = Input.GetButton("Vertical");
        if (isVerticalPressed)
        {
            if (!thrust.enabled)
            {
                thrust.enabled = true;
            }
        }
        else
        {
            if (thrust.enabled)
            {
                thrust.enabled = false;
            }
        }
    }
}
