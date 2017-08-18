using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThruster : MonoBehaviour {
    public SpriteRenderer thrust;
    // Use this for initialization
    void Start () {
        thrust = GetComponent<SpriteRenderer>();
        thrust.enabled = true;
	}
}
