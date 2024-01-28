using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using Random=UnityEngine.Random;
using UnityEngine.SceneManagement;

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
    public bool playVictoryMusic = false;
    public bool playCreditsMusic = false;    
    public bool musicIsActive = false;



    public static AudioManager instance;


    void Awake(){

        if (instance == null)
            instance = this;
        else {
            DestroyImmediate(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);

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
        /*
        if (playFightMusic){
            Sound s = Array.Find(Music, sound => sound.name == "IntroductoryMusic");
            s.source.Play();
            introductoryMusicIsPlaying = true;            
        }
        */
        if (playMainMenuMusic){
            Sound s = Array.Find(Music, sound => sound.name == "MainMenuMusic");
            s.source.Play();
        }
        
        introFightMusic = Array.Find(Music, sound => sound.name == "IntroductoryMusic");
        fightMusic = Array.Find(Music, sound => sound.name == "MainMusic");
        introVictoryMusic = Array.Find(Music, sound => sound.name == "IntroVictoryMusic");
        victoryMusic = Array.Find(Music, sound => sound.name == "VictoryMusic");
        mainMenuMusic = Array.Find(Music, sound => sound.name == "MainMenuMusic");
        creditsMusic = Array.Find(Music, sound => sound.name == "CreditsMusic");
    }

    public Sound introFightMusic;
    public Sound fightMusic;

    public Sound introVictoryMusic;
    public Sound victoryMusic;

    public Sound mainMenuMusic;
    public Sound creditsMusic;

    void Update(){



        //PLAYS FIGHT MUSIC
        if (playFightMusic && !musicIsActive){
            musicIsActive = true;
            if (!introFightMusic.source.isPlaying){
               introFightMusic.source.Play(); 
            }
            
        } else if (playFightMusic && musicIsActive && !introFightMusic.source.isPlaying){
            if (!fightMusic.source.isPlaying){
                fightMusic.source.Play();
            }
        } 

        //PLAYS VICTORY MUSIC
        if (playVictoryMusic && !musicIsActive){
            musicIsActive = true;
            if (!introVictoryMusic.source.isPlaying){
               introVictoryMusic.source.Play(); 
            }
            
        } else if (playVictoryMusic && musicIsActive && !introVictoryMusic.source.isPlaying){
            if (!victoryMusic.source.isPlaying){
                victoryMusic.source.Play();
            }
        } 

        //PLAYS MAIN MENU MUSIC
        if (playMainMenuMusic && !musicIsActive){
            musicIsActive = true;
            if (!mainMenuMusic.source.isPlaying){
               mainMenuMusic.source.Play(); 
            }
        }

        //PLAYS CREDITS MUSIC
        if (playCreditsMusic && !musicIsActive){
            musicIsActive = true;
            if (!creditsMusic.source.isPlaying){
               creditsMusic.source.Play(); 
            }
        }
        
    }



    public void StopFightMusic(){
        musicIsActive = false;
        playFightMusic = false;
        fightMusic.source.Stop();
        introFightMusic.source.Stop();
    }

    public void StopVictoryMusic(){
        musicIsActive = false;
        playVictoryMusic = false;
        victoryMusic.source.Stop();
        introVictoryMusic.source.Stop();        
    }

    public void StopMainMenuMusic(){
        musicIsActive = false;
        playMainMenuMusic = false;
        mainMenuMusic.source.Stop();
    }

    public void StopCreditsMusic(){
        musicIsActive = false;
        playCreditsMusic = false;
        creditsMusic.source.Stop();
    }



        
    public void TurnFightMusicOn(){
        playFightMusic = true;
    }

    public void TurnMainMenuMusicOn(){
        playMainMenuMusic = true;
    }

    public void TurnCreditsMusicOn(){
        playCreditsMusic = true;
    }




    //FOR CALLING SOUNDS FROM OTHER GAMEOBJECTS
    //FindObjectOfType<AudioManager>().Play("SoundName");

}
