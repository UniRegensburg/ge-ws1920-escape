using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{

    Animator anim;

    //public Transform likt;
    public Transform eyesTarget;

   // public float ikWeight;

    public float lookIKweight;
    public float bodyIKweight;
    public float headIKweight;
    public float eyesIKweight;
    public float clamp;

    public float blend;


    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

      
        
        float f = Random.Range(0.0f, 1.0f);
        anim.SetFloat("Blend", f);
		
	}

    private void OnAnimatorIK(int layerIndex)
    {

       
        anim.SetLookAtWeight(lookIKweight, bodyIKweight, headIKweight, eyesIKweight, clamp);
        anim.SetLookAtPosition(eyesTarget.position);

        /*
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, ikWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, ikWeight);

        anim.SetIKPosition(AvatarIKGoal.LeftFoot, likt.position);
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rikt.position);
        */
    }

}
