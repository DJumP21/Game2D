using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainUI : MonoSingleton2<MainUI>
{
    public Text health;
    public Text score;
    public GameObject settingPanel;

    // Update is called once per frame
    void Update()
    {
        health.text ="Health:"+GameController.Instance.playerHealth;
        score.text = "Score："+GameController.Instance.score;
    }

    public void OnClickMenu()
    {
        settingPanel.SetActive(true);
    }
    public void OnClickSelectLevel()
    {
        GameController.Instance.LoadScene("SelectLevel");
    }
    public void OnClickExit()
    {
        GameController.Instance.ExitGame();
    }
    public void OnClickCancle()
    {
        GameObject settingPanel = GameObject.Find("SettingPanel");
        if (settingPanel.activeSelf==true)
        {
            settingPanel.SetActive(false);
        }
    }
}
