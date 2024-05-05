using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour{
    public AudioSound[] sounds;

    public static AudioManager instance; 

    void Start(){
        Play("BackGround");
    }
    void Awake(){   
        if(instance == null ){
        instance = this;
        }
        else{
            Destroy(gameObject); 
            return; 
        }

        DontDestroyOnLoad(gameObject);
        foreach(AudioSound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;

            s.source.volume = s.volume; 
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void Play(string name){
        AudioSound s = System.Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
