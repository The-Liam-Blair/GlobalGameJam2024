using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScript : MonoBehaviour
{
    public GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 20f * Time.deltaTime);
        if (transform.position.y > 800)
        {
            gm.GetComponent<ExitMenusScript>().ExitCredits();
        }
    }
}
