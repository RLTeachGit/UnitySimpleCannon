using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoKill : MonoBehaviour {

    public float TimeToLive=1.0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, TimeToLive);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
