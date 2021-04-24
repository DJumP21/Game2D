using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton2<LevelManager>
{
    //记录关卡信息
    public Level[] levels;
    public int currentlevel;
    public GameObject selectUI;
    public GameObject selectLevel;
    // Start is called before the first frame update
    private void Start()
    {
        currentlevel = 0;
    }
    void Update()
    {
        selectUI = GameObject.FindGameObjectWithTag("SelectUI");
        MessageBoxManager.Init(selectUI);
        selectLevel = selectUI.transform.GetChild(0).GetChild(0).gameObject;
        levels = selectLevel.GetComponentsInChildren<Level>();
        foreach (Level level in levels)
        {
            level.islock = true;
        }
        levels[currentlevel].islock = false;
    }

    
}
