using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    private bool paused;

    private int selected;
    public RawImage selector;

    public TMP_Text button1;
    public TMP_Text button2;

    public GameObject pauseMenu;

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
                pauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                paused = false;
                pauseMenu.SetActive(false);
            }
        }

        if (paused == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (selected == 1)
                {
                    selected = 2;
                    selector.transform.position = new Vector3(button2.transform.position.x - 5, button2.transform.position.y, button2.transform.position.z);
                }
                else
                {
                    selected = 1;
                    selector.transform.position = new Vector3(button1.transform.position.x - 5, button1.transform.position.y, button1.transform.position.z);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (selected == 1)
                {
                    selected = 2;
                    selector.transform.position = new Vector3(button2.transform.position.x - 5, button2.transform.position.y, button2.transform.position.z);
                }
                else
                {
                    selected = 1;
                    selector.transform.position = new Vector3(button1.transform.position.x - 5, button1.transform.position.y, button1.transform.position.z);
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (selected == 1)
                {
                    //Restart
                    SceneManager.LoadScene("GameUIScene");
                }
                else if (selected == 2)
                {
                    //Menu
                    SceneManager.LoadScene("MainMenuScene");
                }
            }
        }
    }
}