using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour {


    public GameObject BallPrefab;

    public Transform FireFrom;
    public Transform FireBase;

    public float MaxFireForce=20.0f;
    public float MinFireForce = 5.0f;
    public float RotateSpeed = 10.0f;

    float mZBaseRotation;

    float mZRotation=0.0f;

    float mFireForce;

    Rigidbody2D mRB;

    public  Image PowerMeterImage;


    // Use this for initialization
    void Start () {
        Debug.Assert(BallPrefab != null, "Ball Prefab missing");
        Debug.Assert(FireFrom != null || FireBase ==null, "FireTranform(s) missing");

        mRB = GetComponent<Rigidbody2D>();
        Debug.Assert(mRB != null, "Rigidbody2D missing");
        mZBaseRotation = mRB.rotation;
        mFireForce = MaxFireForce * 0.2f;
        UpdatePower(0.0f);
    }

    // Update is called once per frame

    bool mFire=false;
	private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            mFire = true;
        }
        if(Input.GetKey(KeyCode.LeftArrow)) {
            UpdatePower(10.0f * Time.deltaTime);
            
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            UpdatePower(-10.0f * Time.deltaTime);
        }
	}

    void    UpdatePower(float vDelta) {
        mFireForce = Mathf.Clamp(mFireForce+vDelta, MinFireForce, MaxFireForce);
    }

	void FixedUpdate () {
        if(mFire) {
            GameObject tGO=Instantiate(BallPrefab, FireFrom.position, Quaternion.identity);
            Rigidbody2D tBallRB = tGO.GetComponent<Rigidbody2D>();
            tBallRB.AddForce((FireFrom.position - FireBase.position).normalized * mFireForce * tBallRB.mass, ForceMode2D.Impulse);
            mFire = false;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            mZRotation += RotateSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            mZRotation -= RotateSpeed * Time.deltaTime;
        }
        mZRotation = Mathf.Clamp(mZRotation, -55.0f, 55.0f);
        mRB.rotation = mZBaseRotation+mZRotation;
    }

    private void LateUpdate() {
        PowerMeterImage.rectTransform.localScale = new Vector2(1.0f - (mFireForce / MaxFireForce), 1);
    }
}
