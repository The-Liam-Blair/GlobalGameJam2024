using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using Random=UnityEngine.Random;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    [Header("--- Player Sounds ---")]
    public Sound[] playerFootsteps;
    public Sound[] playerJumps;
    public Sound[] playerWoosh;
    public Sound[] playerHit;

    [Header("--- Music ---")]
    public Sound[] Music;
    public float victoryMusicDelay;
    private bool introductoryMusicIsPlaying = false;
    private bool introVictoryMusicIsPlaying = false;
    private bool playLoopVictoryMusic = false;

    [Header("--- Scene Music ---")]
    public bool playFightMusic = false;
    public bool playMainMenuMusic = false;


    


    void Awake(){
        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        }
        foreach (Sound s in playerJumps){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        }
        foreach (Sound s in playerWoosh){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        }
        foreach (Sound s in playerHit){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        }
        foreach (Sound s in playerFootsteps){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        }
        foreach (Sound s in Music){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        }

    }



    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Stop();
    }

    public void PlayJumpSound(){
        Sound s = playerJumps[Random.Range(0, playerJumps.Length)];
        s.source.Play();
    }

    public void PlayerWhoosh(){
        Sound s = playerWoosh[Random.Range(0, playerWoosh.Length)];
        s.source.Play();    
    }

    public void PlayerHit(){
        Sound s = playerHit[Random.Range(0, playerHit.Length)];
        s.source.Play();    
    }

    public void PlayerFootSteps(){
        Sound s = playerFootsteps[Random.Range(0, playerFootsteps.Length)];
        s.source.Play();        
    }

    void Start(){
        //SOUNDS/MUSIC WANTING TO BE PLAYED FROM START SHOULD BE CALLED FROM HERE
        if (playFightMusic){
            Sound s = Array.Find(Music, sound => sound.name == "IntroductoryMusic");
            s.source.Play();
            introductoryMusicIsPlaying = true;            
        }
        if (playMainMenuMusic){
            Sound s = Array.Find(Music, sound => sound.name == "MainMenuMusic");
            s.source.Play();
        }

        introVicMusic = Array.Find(Music, sound => sound.name == "IntroVictoryMusic");

        
    }

    void Update(){
        PlayFightSceneMainMusic();
        CheckifActivateVictoryLoop();
    }






    void PlayFightSceneMainMusic(){
        if (playFightMusic){
            Sound s = Array.Find(Music, sound => sound.name == "IntroductoryMusic");
            if (s.source.isPlaying)
            {
                //Debug.Log("Music is playing");
            } else if (!s.source.isPlaying && introductoryMusicIsPlaying){
                introductoryMusicIsPlaying = false;
                Sound s2 = Array.Find(Music, sound => sound.name == "MainMusic");
                bool DoOnce = false;
                if (!DoOnce){
                    s2.source.Play();
                    DoOnce = true;
                }
            }
        }
    }

    public void StopFightSceneMainMusic(){
        playFightMusic = false;
        Sound s = Array.Find(Music, sound => sound.name == "IntroductoryMusic");
        s.source.Stop();
        Sound s2 = Array.Find(Music, sound => sound.name == "MainMusic");
        s2.source.Stop();     
        Debug.Log("STOP FIGHT MUSIC");
    }

    private Sound introVicMusic;
    private bool activateVictoryMusic = false;
    public bool isIntroVicPlaying = false;

    public void PlayVictorySceneMusic(){
        if (!activateVictoryMusic){
            introVicMusic.source.Play();
            activateVictoryMusic = true;
            isIntroVicPlaying = true;
        }
    }

    void CheckifActivateVictoryLoop(){
        if (isIntroVicPlaying && !introVicMusic.source.isPlaying){
            isIntroVicPlaying = false;
            Sound s = Array.Find(Music, sound => sound.name == "VictoryMusic");
            s.source.Play();
        }
    }

    //FOR CALLING SOUNDS FROM OTHER GAMEOBJECTS
    //FindObjectOfType<AudioManager>().Play("SoundName");

}
