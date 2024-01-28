using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitMenusScript : MonoBehaviour
{
    public GameObject blackSquare;
        private void Start()
    {
        StartCoroutine(FadeOutToBlack());
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) 
        {
            StartCoroutine(FadeToBlack());
        }

    }

    public void ExitCredits()
    {
        StartCoroutine(FadeToBlack());
    }

    public IEnumerator FadeToBlack(int speed = 1)
    {
      //  Debug.Log("Run Fade to Black");
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

        SceneManager.LoadScene("MainMenuScene");
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
        yield return null;
    }

}
