using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MessageBox : MonoBehaviour
{
    public Text content;

    // Start is called before the first frame update
    void Start()
    {
        content.text = "您选择的关卡尚未解锁，请通关当前关卡进行解锁。";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickButton()
    {
        Destroy(this.gameObject);
        MessageBoxManager.messageBox = null;
    }
}
