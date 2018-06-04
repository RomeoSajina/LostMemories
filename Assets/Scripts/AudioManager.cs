using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static readonly string FAST_SUFIX = "_fast";

    private static readonly List<String> surfaces = new List<String>() { "wood", "carpet", "grass", "snow", "concrete"};

    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    void Awake () {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //Sound[] tmp = new Sound[sounds.Length*2];
        //int i = 0;

        foreach (Sound s in sounds) {
            
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;


            s.source.outputAudioMixerGroup = mixerGroup;
            /*
            Sound c = new Sound
            {
                source = gameObject.AddComponent<AudioSource>(),
                name = s.name + FAST_SUFIX
            };
            c.clip = s.clip;
            c.loop = s.loop;
            c.volume = s.volume;
            c.pitch = s.pitch + 0.5f;
            c.source.clip = s.clip;
            c.source.loop = s.loop;
            c.source.volume = s.volume;
            c.source.pitch = s.pitch;


            tmp[i] = s;
            tmp[i + sounds.Length] = c;
            i++;*/
        }
        //sounds = tmp;

        Sound[] running = createRunningSurfaces();
        Sound[] tmp = new Sound[sounds.Length + running.Length];
        
        Array.Copy(running, tmp, running.Length);
        Array.Copy(sounds, 0, tmp, running.Length, sounds.Length);
        sounds = tmp;
    }
  
    public void Play(string name){
        Sound s = Array.Find(sounds, item => item.name == name);
        if (s == null){
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (s.source.isPlaying) return;

        Debug.Log("Playing: " + name);
                
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        
        s.source.Play();
    }

    public void Stop (string sound) {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (!s.source.isPlaying) return;

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Stop();
    }

    public void StopAll () {
        foreach (Sound s in sounds) {
            if (s.source != null)
                s.source.Stop();
            else
                Debug.Log("Source of: "+s.name+" not found!");
        }
    }

    private Sound[] createRunningSurfaces() {

        Sound[] surfaceSounds = new Sound[surfaces.Count];
        int i = 0;

        foreach (Sound s in sounds) {

            /*Dupliciraj samo zvukove za povrsine*/
            if (surfaces.FindIndex(a => a == s.name) == -1) continue;

            Sound c = new Sound {
                source = gameObject.AddComponent<AudioSource>(),
                name = s.name + FAST_SUFIX
            };

            c.clip = s.clip;
            c.loop = s.loop;
            c.volume = s.volume;
            c.pitch = s.pitch + 0.5f;
            c.source.clip = s.clip;
            c.source.loop = s.loop;
            c.source.volume = s.volume;
            c.source.pitch = s.pitch;

            surfaceSounds[i] = c;
            i++;
        }

        return surfaceSounds;
    }

    public bool IsSurfaceTag(string tag){
        return surfaces.FindIndex(s => tag.StartsWith(s)) != -1;
        //return tag.StartsWith("wood") || tag.StartsWith("carpet") || tag.StartsWith("grass") || tag.StartsWith("snow") || tag.StartsWith("concrete");
    }
}
