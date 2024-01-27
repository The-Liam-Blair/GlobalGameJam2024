using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControlScript : MonoBehaviour
{

    public void playFunction()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("GameScene");
    }
    public void instuctionFunction()
    {
        Debug.Log("Instruction");
        SceneManager.LoadScene("InstructionsScene");
    }
    public void creditsFunction()
    {
        Debug.Log("Credits");
        SceneManager.LoadScene("CreditsScene");

    }
    public void exitFunction()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
