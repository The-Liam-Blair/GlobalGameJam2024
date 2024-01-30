using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScript : MonoBehaviour
{
    public GameObject gm;
    // Start is called before the first frame update
    private float startPos;
    private float endPos;
    private float creditsHeight;

    void Start()
    {
        Debug.Log("Screen Height : " + Screen.height);
        creditsHeight = this.GetComponent<RectTransform>().rect.height;
        Debug.Log("Height " + creditsHeight);

        startPos = (((Screen.height/2) + (creditsHeight/2))); //  add on the length of the credits object
        Debug.Log("start pos " + startPos);
        
        endPos = (Screen.height / 2);
//        transform.position = new Vector3(transform.position.x, startPos);
        Debug.Log("pos " + transform.position);

        transform.position = new Vector3(transform.position.x, transform.position.y - startPos);
        Debug.Log("pos " + transform.position);

    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 30f * Time.deltaTime);
        if (transform.position.y > startPos)
        {
            gm.GetComponent<ExitMenusScript>().ExitCredits();
        }
    }
}
