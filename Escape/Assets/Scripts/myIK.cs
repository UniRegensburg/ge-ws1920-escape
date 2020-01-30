using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myIK : MonoBehaviour
{

    public Transform target;
    public Transform root;
    float maxRotation = 50;
    
    void LateUpdate()
    {
              
        Vector3 relativePos = target.position - transform.position; 


        //Quaternion source = Quaternion.LookRotation(root.position, Vector3.up);
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        

        float angle = Vector3.Angle(relativePos, root.forward);
        
        if (angle > -maxRotation && angle <maxRotation)
        {
            transform.rotation = rotation;
            //transform.rotation = Quaternion.Slerp(source, rotation,Time.deltaTime);
            transform.Rotate(0, 0, -90);
        }
       




        /*
        Vector3 relativePos = target.position - transform.position;
        float step = speed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        */

    }
}
