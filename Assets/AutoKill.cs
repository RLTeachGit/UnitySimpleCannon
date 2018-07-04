using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoKill : MonoBehaviour {

    public float TimeToLive=1.5f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, TimeToLive);
	}

    private void OnTriggerExit2D(Collider2D vCollision) {
        if(vCollision.gameObject.GetComponent<Camera>()!=null) {    //If we leave Camera Box collider we die
            Destroy(gameObject);
        }
    }
}
