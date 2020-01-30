using UnityEngine;

public class PlayStepSound : MonoBehaviour
{

    public AudioClip ac1;
    public AudioClip ac2;

    AudioSource audio;


    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Step1()
    {      
        audio.PlayOneShot(ac1);
    }

    private void Step2()
    {
        audio.PlayOneShot(ac2);
    }
}