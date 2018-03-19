using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {


    public GameObject BallPrefab;
    Rigidbody2D mRB;

    public Transform FireFrom;
    public Transform FireBase;

    public float FireForce=10.0f;

    // Use this for initialization
    void Start () {
        mRB = GetComponent<Rigidbody2D>();
        Debug.Assert(BallPrefab != null, "Ball Prefab missing");
        Debug.Assert(FireFrom != null || FireBase ==null, "FireTranform(s) missing");
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Space)) {
            GameObject tGO=Instantiate(BallPrefab, FireFrom.position, Quaternion.identity);
            Rigidbody2D tBallRB = tGO.GetComponent<Rigidbody2D>();
            tBallRB.AddForce((FireFrom.position - FireBase.position).normalized * FireForce, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, 1.0f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(0, 0, -1.0f);
        }
    }
}
