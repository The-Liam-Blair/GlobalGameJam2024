using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GMScript : MonoBehaviour
{
    public Slider p1Health;
    public Slider p2Health;

    public GameObject VictoryMenu;
    public TMP_Text P1Victory;
    public TMP_Text P2Victory;

    private int selected;

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

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    p1Health.GetComponent<HealthBarScripts>().Health(10);
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    p2Health.GetComponent<HealthBarScripts>().Health(10);
        //}

        if (VictoryMenu.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (selected == 1)
                {
                    selected = 2;
                }
                else
                {
                    selected = 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (selected == 1)
                {
                    selected = 2;
                }
                else
                {
                    selected = 1;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (selected == 1)
                    {
                        //RESTART FIGHT
                    }
                    else
                    {
                        //BACK TO MENU
                    }
                }

            }
        }
    }
}