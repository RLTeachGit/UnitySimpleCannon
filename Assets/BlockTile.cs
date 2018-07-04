using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class BlockTile : MonoBehaviour {
    
    Rigidbody2D     mRB;        //These are exposed on the editor
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

    public bool TakeDamage = true;

    public  Weights Weight = Weights.Light;
    public Sprite[] AltSprites;


    private Weights mOldWeight = Weights.Undefined;

    public float Health = 1.0f;

    // Use this for initialization
	void Start () {
        mRB = GetComponent<Rigidbody2D>();          //Cache referenced to key compoenents for speed
        mSR = GetComponent<SpriteRenderer>();
        SetWeigth(Weight);
        CalculateDamage(0.0f);
	}
	
	// Update is called once per frame
	void Update () {
        SetWeigth(Weight);
	}

    void    SetWeigth(Weights vNewWeight) {     //Dynamically sets physics weights based on block type
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


    protected virtual void    BeenHit(Collision2D vCollision) {
        if (!TakeDamage) return;        //Some blocks dont take damage
        if (vCollision.gameObject.tag == "CannonBall") {        //What were we hit by?
            float tForce = vCollision.relativeVelocity.magnitude * vCollision.otherRigidbody.mass;  //Do Canon ball damage
            CalculateDamage(tForce);
            MakeSmoke((Vector2)vCollision.contacts[0].point, Mathf.Atan2(vCollision.contacts[0].normal.y, vCollision.contacts[0].normal.x));
        } else if (vCollision.gameObject.tag == "CannonBall") {
            float tForce = vCollision.relativeVelocity.magnitude * vCollision.otherRigidbody.mass;  //do Fall damage but ignore small falls
            if(tForce>2.0f) {
                CalculateDamage(30.0f); //However to lots of damage if valiud fall
            }
        }
    }

    void OnCollisionEnter2D(Collision2D vCollision) {
        BeenHit(vCollision);
    }



    void    MakeSmoke(Vector2 vPoint, float vAngle) {       //Trigger particles
        GameObject tGO=Instantiate(PSPrefab);
        Destroy(tGO, 1.5f);
        tGO.transform.position = vPoint;
        tGO.transform.Rotate(0, 0, vAngle*Mathf.Rad2Deg);
    }

    void    CalculateDamage(float vForce) {     //Work out which bloick to show when damaged
        if (!TakeDamage) return;        //Some blocks dont take damage
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

    private void OnTriggerExit2D(Collider2D vCollision) {       //Kill objects which leave game world
        if (vCollision.gameObject.GetComponent<Camera>() != null) {    //If we leave Camera Box collider we die
            Destroy(gameObject);
        }
    }
}
