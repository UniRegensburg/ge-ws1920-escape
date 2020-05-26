using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Manager : MonoBehaviour
{

    public bool isOpen;
    public bool isLocked;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Open()
    {
        if (isLocked==false)
        {
           // anim.SetBool("open", true);
            anim.SetTrigger("open");
            
        }
	}

    public void Close()
    {
        //anim.SetBool("open", false);
        anim.SetTrigger("close");
    }



}
