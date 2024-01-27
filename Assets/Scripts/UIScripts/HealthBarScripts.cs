using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HealthBarScripts : MonoBehaviour
{
    public Slider healthBar;

    private int health = 100;

    void Start()
    {
        healthBar.maxValue = health;
    }

    void Update()
    {
        healthBar.value = health;
    }

    public void Health(int damage)
    {
        health = health - damage;
    }
}