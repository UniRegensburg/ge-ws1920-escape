using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;




public class NPC_Behavior : MonoBehaviour
{
    // 
    NPC_Animation anim;
    float likesPlayer;
    int nwt;

    public List<GameObject> walkTargets;


    //NPC_Data data;
    //private string filePath;

    //public List<Transform> locations;


    void Start()
    {
        anim = GetComponent<NPC_Animation>();


        /*
        filePath = Application.dataPath + "/NPC_Data/" + gameObject.name + ".json";
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            Debug.Log(dataAsJson);
        }
            anim = GetComponent<NPC_Animation>();
            */

    }
    

    void Update()
    {
       

            //LoadNPCData();
        if (Input.GetKeyDown(KeyCode.O))
        {
            anim.WalkTo(walkTargets[nwt]);
            nwt++;

        }
        
    }

    public void TurnTo(GameObject turnTarget)
    {
        anim.TurnTo(turnTarget);
    }

    public void StartConversation(GameObject turnTarget)
    {
        //anim.WalkTo(null);
        anim.TurnTo(turnTarget);
    }

    

    /*
    public void Look(GameObject lookTarget)
    {
        anim.lookTarget = lookTarget.transform;
    }
    /*
    private void LoadNPCData()
    {
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            //data = JsonUtility.FromJson<NPC_Data>(dataAsJson);
            Debug.Log(data);
        }
        else
        {
            data = new NPC_Data();
            data.name = gameObject.name;
            SaveNPCData();
        }
    }

    private void SaveNPCData()
    {
        
        
        Debug.Log("Saving");
        string dataAsJson = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, dataAsJson);
        
    }

    */
}
