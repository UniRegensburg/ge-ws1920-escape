using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject overlay;
    public GameObject objectInFocus;

    public List<String> inventory;

    public GameObject playerController;
    FirstPersonController playerMovement;

    public Text objektname;
    public Text interaktion;

    int layer = 1 << 9;

    void Start()
    {
        DisableUI();
        playerMovement = playerController.GetComponent<FirstPersonController>();
    }

    void Update()
    {
        CheckPlayerInput();
        CheckObjectNear();

    }

    private void CheckObjectNear()
    {
        RaycastHit rHit;

        //Object in sight
        if (Physics.Raycast(transform.position, transform.forward, out rHit, 5, layer))
        {
            objectInFocus = rHit.collider.gameObject;
            objektname.text = rHit.collider.gameObject.name;

            EnableUI();

            if (rHit.collider.tag == "Interactable")
            {
                interaktion.text = "take";

                if (Input.GetKeyUp(KeyCode.E))
                {
                    inventory.Add(objectInFocus.name);
                    Destroy(objectInFocus);
                }
            }

            else if (rHit.collider.tag == "Door")
            {
                interaktion.text = "open / close";
                

                if (Input.GetKeyUp(KeyCode.E))
                {
                    Door_Manager dm = objectInFocus.GetComponent<Door_Manager>();
                    dm.OpenClose();
                }

            }
        }

        //No Object in sight
        else
        {
            DisableUI();
        }
    }

    private void CheckPlayerInput()
    {
        if (Input.GetKeyUp(KeyCode.M)) //Inventory
        {
           // playerMovement.SwitchMouseLook();
        }

    }

    void EnableUI()
    {
        overlay.SetActive(true);
    }

    void DisableUI()
    {
        overlay.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
   