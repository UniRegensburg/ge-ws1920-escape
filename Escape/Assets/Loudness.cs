using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loudness : MonoBehaviour
{

    AudioSource audioSource;
    public float updateStep = 0.001f;
    public int sampleDataLength = 256;

    private float currentUpdateTime = 0f;

    private float clipLoudness;
    private float[] clipSampleData;

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        
        clipSampleData = new float[sampleDataLength];
    }


    void Update()
    {
        
        currentUpdateTime += Time.deltaTime;

        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }

            clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
            Debug.Log(clipLoudness);
            transform.localScale = new Vector3(clipLoudness, clipLoudness, clipLoudness );

        }
        
    }

}
