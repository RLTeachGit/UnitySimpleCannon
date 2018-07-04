using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class BlockTile : MonoBehaviour {
    
    Rigidbody2D     mRB;
    SpriteRenderer  mSR;
    public GameObject  PSPrefab;

    public  bool Fixed=true;
    public  enum Weights {
        Light
        ,Heavy
        ,VeryHeavy
        ,Fixed
        ,Undefined
    }

    public  Weights Weight = Weights.Light;
    public Sprite[] AltSprites;


    private Weights mOldWeight = Weights.Undefined;

    public float Health = 1.0f;

    // Use this for initialization
	void Start () {
        mRB = GetComponent<Rigidbody2D>();
        mSR = GetComponent<SpriteRenderer>();
        SetWeigth(Weight);
        CalculateDamage(0.0f);
	}
	
	// Update is called once per frame
	void Update () {
        SetWeigth(Weight);
	}

    void    SetWeigth(Weights vNewWeight) {
        if(vNewWeight!=mOldWeight) {
            switch (vNewWeight) {
                case Weights.Light:
                    mRB.isKinematic = false;
                    mRB.mass = 10.0f;
                    break;

                case Weights.Heavy:
                    mRB.isKinematic = false;
                    mRB.mass = 10.0f;
                    break;

                case Weights.VeryHeavy:
                    mRB.isKinematic = false;
                    mRB.mass = 10.0f;
                    break;

                case Weights.Fixed:
                    mRB.isKinematic = true;
                    mRB.mass = 1.0f;
                    break;

                default:
                    Debug.LogError("Undefined Weight");
                    return;

            }
            mOldWeight = Weight;
            Weight = vNewWeight;
        }
    }


    void OnCollisionEnter2D(Collision2D vCollision) {
        if(vCollision.gameObject.tag=="CannonBall") {
            float tForce = vCollision.relativeVelocity.magnitude*vCollision.otherRigidbody.mass;
            CalculateDamage(tForce);
            MakeSmoke((Vector2)vCollision.contacts[0].point,Mathf.Atan2(vCollision.contacts[0].normal.y, vCollision.contacts[0].normal.x));
        }
    }



    void    MakeSmoke(Vector2 vPoint, float vAngle) {
        GameObject tGO=Instantiate(PSPrefab);
        Destroy(tGO, 1.5f);
        tGO.transform.position = vPoint;
        tGO.transform.Rotate(0, 0, vAngle*Mathf.Rad2Deg);
    }

    void    CalculateDamage(float vForce) {
        Health = Health - (vForce / 100.0f);
        if(Health>=0.6f) {
            mSR.sprite = AltSprites[0];            
        } else if(Health>=0.3f) {
            mSR.sprite = AltSprites[1];            
        } else if (Health >= 0.0f ) {
            mSR.sprite = AltSprites[2];
        } else {
            SetWeigth(Weights.Heavy);
        }
    }

    private void OnTriggerExit2D(Collider2D vCollision) {
        if (vCollision.gameObject.GetComponent<Camera>() != null) {    //If we leave Camera Box collider we die
            Destroy(gameObject);
        }
    }
}
