using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    private float x;
    private float y;
    private float z;
    private float dis;
    // Start is called before the first frame update
    private void Start()
    {
        z = this.transform.position.z;
    }
    // Update is called once per frame
    void Update()
    {
        x = Player.transform.position.x;
        y = Player.transform.position.y;
        this.transform.position = new Vector3(x, y,z);
    }
}
