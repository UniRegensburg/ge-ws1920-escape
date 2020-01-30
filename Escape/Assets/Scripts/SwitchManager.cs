using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{

    public GameObject lights;

	public void turnOnOff()
    {
        lights.SetActive(!lights.activeSelf);

    }
}
