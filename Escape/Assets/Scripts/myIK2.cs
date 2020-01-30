using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myIK2 : MonoBehaviour
{

    public Transform target;
    public Transform root;
    float speed = 1.0f;

    // Start is called before the first frame update
    private void Start()
    {
       
    }


    // Update is called once per frame
    void LateUpdate()
    {

       

       
        Vector3 relativePos = target.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        float angle = Vector3.Angle(relativePos, root.forward);
        
        if (angle > -60 && angle < 60)
        {
            transform.rotation = rotation;
            transform.Rotate(0, 0, -90);
        }
       




        /*
        Vector3 relativePos = target.position - transform.position;
        float step = speed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        */

    }
}
