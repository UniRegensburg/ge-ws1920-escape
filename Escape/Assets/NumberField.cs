using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class NumberField : MonoBehaviour
{
    public string number;
   
    GameObject g; 
    
    void Start()
    {
        number = "";
    }

    private void Update()
    {

    }


    public void AddNumber(string n)
    {
        number += n;
    }

   
}
