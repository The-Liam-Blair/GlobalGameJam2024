using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    [SerializeField]
    private bool ScriptActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

            if (Input.GetKeyUp(KeyCode.P)){
                FindObjectOfType<AudioManager>().StopFightSceneMainMusic();
                FindObjectOfType<AudioManager>().PlayVictorySceneMusic();
                
            } 
        }

    }
}
