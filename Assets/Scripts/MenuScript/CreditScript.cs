using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditScript : MonoBehaviour
{
    public GameObject gm;
    private float timer = 0;
    private float speed;

    void Start()
    {
        // calculate the speed of the credits based on screen hight 
        // this helps keep it consistet across screen sizes
        speed = Screen.height / 10;
    }

    void FixedUpdate()
    {
        timer += 1 * Time.deltaTime;
        if (timer > 25)
        {
            gm.GetComponent<ExitMenusScript>().ExitCredits();
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime), transform.position.z);
    }
}
