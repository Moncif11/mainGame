using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioSound
{
    public String name; 

    public AudioClip audioClip; 
    [Range(0f,5)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;

    public bool loop; 

    [HideInInspector]
    public AudioSource source;  
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
