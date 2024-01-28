using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControlScript : MonoBehaviour
{

    public GameObject blackSquare;

    private void Start()
    {
        //        blackSquare.SetActive(true);
        StartCoroutine(FadeOutToBlack());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(FadeToBlack("Test"));
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(FadeOutToBlack());
        }

    }
    public IEnumerator FadeToBlack(string sceneName, int speed = 1)
    {
     //   Debug.Log("Run Fade to Black");
        // Fade to black then start new scene
        blackSquare.SetActive(true);
        Color objCol = blackSquare.GetComponent<Image>().color;
        float fadeAmount;
        while (blackSquare.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objCol.a + (speed * Time.deltaTime);
            objCol = new Color(objCol.r, objCol.g, objCol.b, fadeAmount);
            blackSquare.GetComponent<Image>().color = objCol;
            yield return null;
        }
        // Now load scene

        SceneManager.LoadScene(sceneName);
    }
    public IEnumerator FadeOutToBlack(int speed = 1)
    {
      //  Debug.Log("Fade OUT to black");
        blackSquare.SetActive(true);
        // Fade from black to play game
        Color objCol = blackSquare.GetComponent<Image>().color;
        float fadeAmount;

        while (blackSquare.GetComponent<Image>().color.a > 0)
        {
            fadeAmount = objCol.a - (speed * Time.deltaTime);
            objCol = new Color(objCol.r, objCol.g, objCol.b, fadeAmount);
            blackSquare.GetComponent<Image>().color = objCol;
            yield return null;
        }
        blackSquare.SetActive(false);

    }

    public void playFunction()
    {
       // Debug.Log("Play");
        StartCoroutine(FadeToBlack("GameScene"));
//        SceneManager.LoadScene("GameScene");
    }
    public void instuctionFunction()
    {
     //   Debug.Log("Instruction");
        StartCoroutine(FadeToBlack("InstructionsScene"));
        //      SceneManager.LoadScene("InstructionsScene");
    }
    public void creditsFunction()
    {
     //   Debug.Log("Credits");
        StartCoroutine(FadeToBlack("CreditsScene"));
        //    SceneManager.LoadScene("CreditsScene");

    }
    public void exitFunction()
    {
      //  Debug.Log("Quit");
        Application.Quit();
    }

}
