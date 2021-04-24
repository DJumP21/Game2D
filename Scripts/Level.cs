using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    public string name;
    public bool islock;
    void Start()
    {
        if (SceneManager.GetActiveScene().name=="SelectLevel")
        {
            this.name = this.gameObject.name;
        }
        else
        {
            this.name = SceneManager.GetActiveScene().name;
        }
        
    }
    public void OnClickSelect()
    {
        GameController.Instance.currentLevel = this;
        if (GameController.Instance.currentLevel.islock==false)
        {
            GameController.Instance.LoadScene(GameController.Instance.currentLevel.name);
        }
        else
        {
            MessageBoxManager.ShowMessageBox();
        }
    }
}
