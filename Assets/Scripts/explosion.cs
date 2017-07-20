using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour {
    public int count;
	// Use this for initialization
	void Start () {
        count = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (count == 5)
            Destroy(gameObject);
        count++;
	}
}
