using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Items
{
    private static string[] allItems =
    {
        "Papertowel",
        "BlaBlaBala",
        "Pocketknife"
    };

   

    public static string GetRandomItem()
    {
        int num = (int)Random.Range(0, allItems.Length);
        string item = allItems[num];
        allItems[num].Remove(num);
        return item;
    }   
}


