using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropNoise : MonoBehaviour
{
    public AudioSource sound;

    void OnCollisionEnter(Collision collision)
    {
        float speed = collision.relativeVelocity.magnitude;
        sound.volume = speed / 2;
        sound.Play();
        
    }
}
