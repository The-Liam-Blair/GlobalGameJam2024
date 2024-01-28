using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    private bool paused;

    private int selected;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
            {
                Time.timeScale = 0;
                paused = true;
            }
            else
            {
                Time.timeScale = 1;
                paused = false;
            }
        }

        if (paused == true && Input.GetKeyDown(KeyCode.E))
        {
            if (selected == 1)
            {
                //Resume
                Time.timeScale = 1;
            }
            else if (selected == 2)
            {
                //Restart
                SceneManager.LoadScene("GameUIScene");
            }
            else
            {
                //Menu
                SceneManager.LoadScene("MainMenuScene");
            }
        }
    }
}