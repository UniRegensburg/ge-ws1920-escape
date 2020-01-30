using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public string[] items;
    public string selected;
    GUIStyle boxStyle;
    GUIStyle buttonStyle;

    


    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width/4, Screen.height / 2,Screen.width/2, Screen.height/2));

        boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.normal.textColor = Color.red;
        boxStyle.fontSize = 30;
        

        buttonStyle = new GUIStyle(GUI.skin.button);
        //buttonStyle.stretchWidth;
        buttonStyle.hover.textColor = Color.red;
        buttonStyle.fontSize = 20;

        GUILayout.Box("sldfg", boxStyle);
       

        for (int i = 0; i < items.Length; i++)
        {
            if (GUILayout.Button(items[i], buttonStyle))
            {
                selected = items[i];
                Debug.Log(selected);
            }

        }   

        GUILayout.EndArea();

       
    }
}

