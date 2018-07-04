using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour {

    BoxCollider2D mExitBox;

    float EdgeMargin = 0.1f;

	// Use this for initialization
	void Start () {
        mExitBox = gameObject.AddComponent<BoxCollider2D>();
        mExitBox.size = new Vector2(Camera.main.aspect * Camera.main.orthographicSize * (2.0f+EdgeMargin), Camera.main.orthographicSize * (2.0f+ EdgeMargin));
        mExitBox.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
