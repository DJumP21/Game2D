using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 部分可移动地图
/// </summary>
public class MapMove : MonoBehaviour
{
   
    public float xMin;
    public float xMax;
    public float moveSpeed=0.1f;
    private bool left=false;
    private bool right = true;
    
    // Start is called before the first frame update
    void Start()
    { 

    }
    // Update is called once per frame
    void Update()
    {
        if (this.transform.localPosition.x >= xMax)
        {
            right = false;
            left = true;
        }
        if (this.transform.localPosition.x <= xMin)
        {
            right = true;
            left = false;
        }
        if (right==true && left==false)
        {
            this.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        
        else if(right == false && left == true)
        {
            this.transform.Translate(-moveSpeed * Time.deltaTime, 0,0);
        }
       
    }
  
}
