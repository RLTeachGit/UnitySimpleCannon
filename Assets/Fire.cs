using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {


    public GameObject BallPrefab;

    public Transform FireFrom;
    public Transform FireBase;

    public float FireForce=10.0f;
    public float RotateSpeed = 10.0f;

    float mZBaseRotation;

    float mZRotation=0.0f;

    Rigidbody2D mRB;


    // Use this for initialization
    void Start () {
        Debug.Assert(BallPrefab != null, "Ball Prefab missing");
        Debug.Assert(FireFrom != null || FireBase ==null, "FireTranform(s) missing");

        mRB = GetComponent<Rigidbody2D>();
        Debug.Assert(mRB != null, "Rigidbody2D missing");
        mZBaseRotation = mRB.rotation;

    }

    // Update is called once per frame

    bool mFire=false;
	private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            mFire = true;
        }
	}

	void FixedUpdate () {
        if(mFire) {
            GameObject tGO=Instantiate(BallPrefab, FireFrom.position, Quaternion.identity);
            Rigidbody2D tBallRB = tGO.GetComponent<Rigidbody2D>();
            tBallRB.AddForce((FireFrom.position - FireBase.position).normalized * FireForce * tBallRB.mass, ForceMode2D.Impulse);
            mFire = false;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            mZRotation += RotateSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            mZRotation -= RotateSpeed * Time.deltaTime;
        }
        Debug.Log(mZRotation);
        mZRotation = Mathf.Clamp(mZRotation, -55.0f, 55.0f);
        mRB.rotation = mZBaseRotation+mZRotation;
    }
}
