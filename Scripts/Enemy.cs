using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float XMin;
    public float XMax;
    public float moveSpeed = 0.1f;
    private bool left = false;
    private bool right = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x >= XMax)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            right = false;
            left = true;
            
        }
        if (this.transform.position.x <= XMin)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            right = true;
            left = false;
           
        }
        if (right == true && left == false)
        {
            this.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }

        else if (right == false && left == true)
        {
            this.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
        //this.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
    }

}
