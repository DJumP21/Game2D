using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBoxManager : MonoBehaviour
{
    private static GameObject uiRoot;
    public static void Init(GameObject uiRoot)
    {
        MessageBoxManager.uiRoot = uiRoot;
    }

    public static MessageBox messageBox;
    /// <summary>
    /// 显示消息框
    /// </summary>
    public static void ShowMessageBox()
    {
        if (messageBox==null)
        {
            GameObject go = (GameObject)Resources.Load("MessageBox");
            if (go!=null)
            {
                Instantiate(go,uiRoot.transform);
                if (go!=null)
                {
                    messageBox = go.GetComponent<MessageBox>();
                }
            }

        }
        else
        {
            messageBox.gameObject.SetActive(true);
        }
    }
}
