using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GMScript : MonoBehaviour
{
    public Slider p1Health;
    public Slider p2Health;

    public GameObject VictoryMenu;
    public TMP_Text P1Victory;
    public TMP_Text P2Victory;

    private int selected;
    public RawImage selector;

    public TMP_Text button1;
    public TMP_Text button2;

    void Start()
    {
        selected = 1;
    }
    
    void Update()
    {
        if (p1Health.value <= 0)
        {
            //Player 1 wins
            Debug.Log("Player 1 win");
            VictoryMenu.SetActive(true);
            P2Victory.enabled = false;
        }
        if (p2Health.value <= 0)
        {
            //Player 2 wins
            Debug.Log("Player 2 win");
            VictoryMenu.SetActive(true);
            P1Victory.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            p1Health.GetComponent<HealthBarScripts>().Health(10);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            p2Health.GetComponent<HealthBarScripts>().Health(10);
        }

        if (VictoryMenu.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (selected == 1)
                {
                    selected = 2;
                    selector.transform.position = new Vector3(button2.transform.position.x - 350, button2.transform.position.y, button2.transform.position.z);
                }
                else
                {
                    selected = 1;
                    selector.transform.position = new Vector3(button1.transform.position.x - 350, button1.transform.position.y, button1.transform.position.z);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (selected == 1)
                {
                    selected = 2;
                    selector.transform.position = new Vector3(button2.transform.position.x - 350, button2.transform.position.y, button2.transform.position.z);
                }
                else
                {
                    selected = 1;
                    selector.transform.position = new Vector3(button1.transform.position.x - 350, button1 .transform.position.y, button1.transform.position.z);
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (selected == 1)
                {
                    //RESTART FIGHT
                    SceneManager.LoadScene("GameUIScene");
                }
                if (selected == 2)
                {
                    //BACK TO MENU
                    SceneManager.LoadScene("MainMenuScene");
                }
            }
        }
    }
}