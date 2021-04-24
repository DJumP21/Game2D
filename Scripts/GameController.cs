using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Game_Status
{
    GameStart,
    GameRunning,
    GameOver
}
/// <summary>
/// 游戏控制器
/// </summary>
public class GameController : MonoSingleton2<GameController>
{


    public Game_Status game_Status;
    public PlayerController player;

    public GameObject loadingPanel;
    public GameObject tipsPanel;
    public GameObject gameStartPanel;
    public Slider slider;
    public Text progress;
    private bool loaded=false;
    //记录角色状态
    Player_Status player_Status;
    public int playerHealth=3;
    //选择关卡UI
    public Transform selectUI;
    private GameObject selectLevelUI;
    public Level currentLevel;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        game_Status = Game_Status.GameStart;
        player_Status = player.player_Status;
        score = 0;
}

    // Update is called once per frame
    void Update()
    {
        if (game_Status == Game_Status.GameStart)
        {
            LoadGame();
        }
        //currentLevel = LevelManager.Instance.levels[LevelManager.Instance.currentlevel];

        //选择关卡UI的设置
        selectLevelUI = FindGameObjet("SelectLevelUI", selectUI);
        if (SceneManager.GetActiveScene().name != "SelectLevel")
        {
            Debug.Log(selectUI.name+selectLevelUI.activeSelf);
            selectLevelUI.SetActive(false);
        }
        if (SceneManager.GetActiveScene().name == "SelectLevel")
        {
            Debug.Log(selectUI.name+selectLevelUI.activeSelf);
            Debug.Log("当前场景" + SceneManager.GetActiveScene().name);
            selectLevelUI.SetActive(true);
        }

    }

    public void OnClickStart()
    {
        GameStart();
    }
    void GameStart()
    {
        this.gameStartPanel.SetActive(false);
        game_Status = Game_Status.GameRunning;
        LoadScene("SelectLevel");
    }
    //加载游戏
    void LoadGame()
    {
        if (loaded==false)
        {
            this.slider.value += Time.deltaTime;
            this.progress.text = "场景加载中..." + "(" + (int)(slider.value*100) + ")";
            if (slider.value >= 1)
            {
                loadingPanel.SetActive(false);
                StartCoroutine(GameLoading());
                loaded = true;
            }
        }
        else
        {
            return;
        }
    }
    IEnumerator GameLoading()
    {
        tipsPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        tipsPanel.SetActive(false);
        gameStartPanel.SetActive(true);
    }

    public object LoadScene(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName);
        return null;
    }

    public GameObject FindGameObjet(string objectName,Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).gameObject.name==objectName)
            {
                return parent.GetChild(i).gameObject;
            }
        }
        return null;
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
