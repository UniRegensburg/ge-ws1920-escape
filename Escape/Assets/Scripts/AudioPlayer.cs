﻿//Adapted from: https://answers.unity.com/questions/652919/music-player-get-songs-from-directory.html


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class AudioPlayer : MonoBehaviour
{
    public enum SeekDirection { Forward, Backward }


    //GUIStyle gs = new GUIStyle();
    
    public AudioSource source;
    public List<AudioClip> clips = new List<AudioClip>();

    [SerializeField] [HideInInspector] private int currentIndex = 0;

    private FileInfo[] soundFiles;
    private List<string> validExtensions = new List<string> { ".ogg", ".wav", ".mp3", ".wma" }; // Don't forget the "." i.e. "ogg" won't work - cause Path.GetExtension(filePath) will return .ext, not just ext.
    private string absolutePath = "Application.dataPath/Assets/Audio"; // relative path to where the app is running - change this to "./music" in your case

    void Start()
        
    {
        //gs.normal.textColor = Color.cyan;
        //being able to test in unity
        if (Application.isEditor) absolutePath = "Assets/Audio";

        if (source == null) source = gameObject.AddComponent<AudioSource>();

        ReloadSounds();
    }

    void OnGUI()
    {

        

        GUILayout.BeginArea(new Rect(Screen.width / 2, Screen.height / 2, 300, 300));

        //GUILayout.Button(clips[currentIndex].name, gs);

        if (GUILayout.Button("Previous"))
        {
            Seek(SeekDirection.Backward);
            PlayCurrent();
        }
        if (GUILayout.Button("Play current"))
        {
            PlayCurrent();
        }
        if (GUILayout.Button("Next"))
        {
            Seek(SeekDirection.Forward);
            PlayCurrent();
        }
        if (GUILayout.Button("Reload"))
        {
            ReloadSounds();
        }

        GUILayout.Button(clips[currentIndex].name) ;
        
        GUILayout.EndArea();
    }

    void Seek(SeekDirection d)
    {
        if (d == SeekDirection.Forward)
            currentIndex = (currentIndex + 1) % clips.Count;
        else
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = clips.Count - 1;
        }
    }

    void PlayCurrent()
    {
        source.clip = clips[currentIndex];
        source.Play();
    }

    void ReloadSounds()
    {
        clips.Clear();
        // get all valid files
        var info = new DirectoryInfo(absolutePath);
        soundFiles = info.GetFiles()
            .Where(f => IsValidFileType(f.Name))
            .ToArray();

        // and load them
        foreach (var s in soundFiles)
            StartCoroutine(LoadFile(s.FullName));
    }

    bool IsValidFileType(string fileName)
    {
        return validExtensions.Contains(Path.GetExtension(fileName));
        // Alternatively, you could go fileName.SubString(fileName.LastIndexOf('.') + 1); that way you don't need the '.' when you add your extensions
    }

    IEnumerator LoadFile(string path)
    {
        WWW www = new WWW("file://" + path);
        print("loading " + path);

        AudioClip clip = www.GetAudioClip(false);
        while (!clip.isReadyToPlay)
            yield return www;

        print("done loading");
        clip.name = Path.GetFileName(path);
        clips.Add(clip);
    }
}