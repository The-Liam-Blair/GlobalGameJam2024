using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testScript : MonoBehaviour
{
    [SerializeField]
    private bool ScriptActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool DoOnce = false;

    void CheckSceneLevel(){
        if (!DoOnce){
            DoOnce = true;
            Scene mScene = SceneManager.GetActiveScene();
            string sceneName = mScene.name;


            
            //FindObjectOfType<AudioManager>().StopMainMenuMusic();


            if (sceneName == "GameUIScene")
            {
                FindObjectOfType<AudioManager>().StopMainMenuMusic();
                FindObjectOfType<AudioManager>().StopCreditsMusic();
                FindObjectOfType<AudioManager>().StopVictoryMusic();
                FindObjectOfType<AudioManager>().TurnFightMusicOn();
                Debug.Log("is fight scene");
            } 
            if (sceneName == "MainMenuScene"){
                Debug.Log("is main menu scene");
                FindObjectOfType<AudioManager>().StopFightMusic();
                FindObjectOfType<AudioManager>().StopCreditsMusic();
                FindObjectOfType<AudioManager>().StopVictoryMusic();
                FindObjectOfType<AudioManager>().TurnMainMenuMusicOn();
            }        
            if (sceneName == "CreditsScene"){
                Debug.Log("is credits scene");
                FindObjectOfType<AudioManager>().StopMainMenuMusic();
                FindObjectOfType<AudioManager>().StopFightMusic();
                FindObjectOfType<AudioManager>().StopVictoryMusic();
                FindObjectOfType<AudioManager>().TurnCreditsMusicOn();
            }
        }


    }

    // Update is called once per frame
    void Update()
    {


        /*
        if (ScriptActive){
            if (Input.GetKeyDown(KeyCode.W))
            {
                FindObjectOfType<AudioManager>().PlayJumpSound();
                //Debug.Log("JumpPlayed");
            }

            if (Input.GetKeyDown(KeyCode.E)){
                FindObjectOfType<AudioManager>().PlayerWhoosh();
                //Debug.Log("PlayerWhoosh");
            } 
            if (Input.GetKeyDown(KeyCode.R)){
                FindObjectOfType<AudioManager>().PlayerHit();
                //Debug.Log("PlayerHit");
            } 
            if (Input.GetKeyDown(KeyCode.S)){
                FindObjectOfType<AudioManager>().Play("PlayerPunchBuildUp");
            } 
            if (Input.GetKeyUp(KeyCode.S)){
                FindObjectOfType<AudioManager>().Stop("PlayerPunchBuildUp");
            } 
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A)){
                FindObjectOfType<AudioManager>().PlayerFootSteps();
            }
            if (Input.GetKeyUp(KeyCode.S)){
                FindObjectOfType<AudioManager>().Stop("PlayerPunchBuildUp");
            } 


            
            if (Input.GetKeyUp(KeyCode.M)){
                SceneManager.LoadScene("MainMenu");
            } 
            if (Input.GetKeyUp(KeyCode.N)){
                SceneManager.LoadScene("FightScene");
            } 
            if (Input.GetKeyUp(KeyCode.B)){
                SceneManager.LoadScene("Credits");
            } 
            if (Input.GetKeyUp(KeyCode.P)){
                FindObjectOfType<AudioManager>().StopFightMusic();
            } 

                
            if (Input.GetKeyUp(KeyCode.O)){
                FindObjectOfType<AudioManager>().StopVictoryMusic();
            }        

            if (Input.GetKeyUp(KeyCode.I)){
                FindObjectOfType<AudioManager>().StopMainMenuMusic();
            }   


            */

            CheckSceneLevel();

        }

    }


}
