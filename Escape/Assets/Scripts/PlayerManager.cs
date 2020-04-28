using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerManager : MonoBehaviour
{ 
    public GameObject playerController;
    FirstPersonController playerMovement;

    GameObject objectInFocus;
    GUIStyle boxStyle;
    GUIStyle buttonStyle;

    bool layoutset;
    bool uienabled;
    bool itemsVisible;

    public VIDE_Assign diag;

    int layer = 1 << 9;



    List<String> inventar;

    public bool HasItem(String item)
    {
        return (inventar.Contains(item));
    }

    public void EmptyInventory()
    {
        inventar.Clear();
    }

  


    void Start()
    {       
        playerMovement = playerController.GetComponent<FirstPersonController>();
        inventar = new List<string>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            itemsVisible = !itemsVisible;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            playerMovement.mouseLook = false;
        }

        Debug.Log(HasItem("key"));
    }


    void OnGUI()
   {
        GUILayout.BeginArea(new Rect(Screen.width / 4, Screen.height / 2, Screen.width / 2, Screen.height / 2));
        
       
        RaycastHit rHit;

       //Object in sight
       if (Physics.Raycast(transform.position, transform.forward, out rHit, 5, layer))
       {
            GameObject objectInFocus = rHit.collider.gameObject; //Referenz auf Objekt im Fokus
            String sort = rHit.collider.tag;

            switch (sort)
            {
                case "female":
                    SetLayout(Color.magenta);
                    break;
                case "male":
                    SetLayout(Color.blue);
                    break;
                

                default:
                    SetLayout(Color.white);
                    break;
            }


            EnableUI();
            itemsVisible = false;
            
            //GUILayout.Box(objectInFocus.name, boxStyle);// Überschrift

            //Object has Dialogue => It's a NPC
            if (objectInFocus.GetComponent<VIDE_Assign>() != null)
            {
                Debug.Log("Dialogue found");
                diag = objectInFocus.GetComponent<VIDE_Assign>();

                if (!VD.isActive) //Neuen Dialog starten
                {
                    GUILayout.Box(objectInFocus.name, boxStyle);
                    if (GUILayout.Button("Ansprechen", buttonStyle))
                    {
                        VD.BeginDialogue(diag); //We've attached a VIDE_Assign to this same gameobject, so we just call the component
                    }
                }


                else //Dialog läuft bereits
                {                    
                    var data = VD.nodeData; //Quick reference

                    /********************            SPIELER                            *******************/

                    if (data.isPlayer) // If it's a player node, let's show all of the available options as buttons
                    {

                        SetLayout(Color.green);
                        GUILayout.Box("Du", boxStyle);

                        for (int i = 0; i < data.comments.Length; i++)
                        {
                            if (GUILayout.Button(data.comments[i], buttonStyle)) //When pressed, set the selected option and call Next()
                            {
                                data.commentIndex = i;
                                VD.Next();
                            }
                        }
                    }

                    /********************            NPC                            *******************/

                    else //if it's a NPC node, Let's show the comment and add a button to continue
                    {
                        GUILayout.Box(objectInFocus.name, boxStyle);// Überschrift
                        if (GUILayout.Button(data.comments[data.commentIndex], buttonStyle))
                        {
                            VD.Next();
                        }
                    }

                    if (data.isEnd) // If it's the end, let's just call EndDialogue
                    {
                        VD.EndDialogue();
                    }
                }               
            }



            //Object doesn't have a Dialogue => No NPC
            else
            {
                GUILayout.Box(objectInFocus.name, boxStyle);

                if (objectInFocus.tag == "Interactable")
                {

                    if (GUILayout.Button("Nehmen", buttonStyle))
                    {
                        inventar.Add(objectInFocus.name);
                        Destroy(objectInFocus);
                    }
                }

                else if (objectInFocus.tag == "Door")
                {
                    if (GUILayout.Button("Öffnen", buttonStyle))
                    {
                        Door_Manager dm = objectInFocus.GetComponent<Door_Manager>();
                        if (inventar.Contains("key")) 
                        {
                            dm.isLocked = false;
                            dm.Open();
                        }
                        
                    }

                }

                else if (objectInFocus.tag == "Switch")
                {
                    if (GUILayout.Button("Einschalten / Ausschalten", buttonStyle))
                    {
                        SwitchManager sm = objectInFocus.GetComponent<SwitchManager>();
                        sm.turnOnOff();
                    }
                }
            }
        }

        else //No Object in sight
        {
            DisableUI();

            if (itemsVisible)
            {
                GUILayout.Box("Inventar", boxStyle);
                
                foreach (string s in inventar)
                {
                    GUILayout.Button(s, buttonStyle);
                }
            }
            
        }

        GUILayout.EndArea();
    }

    

    private void SetLayout(Color c)
    {
        
        boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.normal.textColor = c;
        boxStyle.fontSize = 24;

        buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.hover.textColor = Color.red;
        buttonStyle.fontSize = 18;
      
    }

    void OnDisable()
    {
        VD.EndDialogue();
        playerMovement.mouseLook = true;
    }

    void EnableUI()
    {
        playerMovement.mouseLook = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //uienabled = true;
    }

    void DisableUI()
    {
        playerMovement.mouseLook = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //uienabled = false;
    }
}
   