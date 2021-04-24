using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 玩家控制器
/// </summary>
public enum Player_Status
{
    Active = 0,
    Hurt = 1,
    Die = 2
}
public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    public float speed;
    private bool left=false;
    private bool right = true;
    public float force=100f;
    private Rigidbody2D playerRb;
    private int jumpCount = 0;
    public GameObject weapon;
    public Transform weaponPoint;
    private Vector2 forward;
    //玩家出生点
    public Transform PlayerBirthPoint;


    //角色状态
    public Player_Status player_Status;
    public GameObject item;
    public bool canAttack=false;
    public Texture2D Boxsprite;
    public bool canClimb=false;

    public Level currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.currentLevel = currentLevel;
        playerAnimator = this.GetComponent<Animator>();
        playerRb = this.GetComponent<Rigidbody2D>();
        player_Status = Player_Status.Active;
    }

    // Update is called once per frame
    void Update()
    {
        //玩家移动
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        else
        {
            playerAnimator.SetBool("Run", false);
        }
        //玩家跳跃
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerJump();
        }
        else
        {
            playerAnimator.SetBool("Jump", false);
        }
        
        //玩家攻击
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerAttack();
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (canClimb==true)
            {
                ClimbUp();
            }
        }else if (Input.GetKey(KeyCode.S))
        {
            if (canClimb == true)
            {
                ClimbDown();
            }
        }
        else
        {
            playerAnimator.SetBool("Climb", false);
        }
        //玩家跌落死亡
        if (this.transform.position.y <= 3.4f)
        {
            if (GameController.Instance.playerHealth >= 0 && this.player_Status == Player_Status.Active)
            {
                StartCoroutine(Die());
            }
            else if (GameController.Instance.playerHealth < 0 && this.player_Status == Player_Status.Die)
            {
                StopCoroutine(Die());
                GameController.Instance.game_Status = Game_Status.GameOver;
                GameController.Instance.LoadScene("GameOver");
            }
            player_Status = Player_Status.Die;
        }
    }

   
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="Map")
        {
            jumpCount = 0;
        }
    }
    GameObject it;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //吃宝石加分数
        if (other.gameObject.tag == "Gem")
        {
            GameController.Instance.score += 10;
            Destroy(other.gameObject);
        }
        //吃宝箱道具
        if (other.gameObject.tag == "Box")
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("Open");
            it = Instantiate(item,other.transform.position,other.transform.rotation);
            Destroy(other.gameObject, 0.5f);
        }
        //吃道具，获得攻击技能
        if (other.gameObject.tag == "Item")
        {
            this.canAttack = true;
            Debug.Log(canAttack);
            Destroy(it, 1f);
        }
        if (other.gameObject.tag=="StairCase")
        {
            canClimb = true;
            Debug.Log("canClimb："+canClimb);
            playerRb.gravityScale = 0;
        }
        if (other.gameObject.tag=="Enemy")
        {
            if (GameController.Instance.playerHealth >= 0 && this.player_Status == Player_Status.Active)
            {
                StartCoroutine(Die());
            }
            else if (GameController.Instance.playerHealth < 0 && this.player_Status == Player_Status.Die)
            {
                StopCoroutine(Die());
                GameController.Instance.game_Status = Game_Status.GameOver;
                GameController.Instance.LoadScene("GameOver");
            }
            player_Status = Player_Status.Die;
        }
        if (other.gameObject.tag == "Transporter")
        {
            LevelManager.Instance.currentlevel++;
            GameController.Instance.LoadScene("SelectLevel");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag== "StairCase")
        {
            canClimb = false;
            playerAnimator.SetBool("Climb", false);
            playerRb.gravityScale = 1;
        }
    }

    public void Hurted()
    {
        playerAnimator.SetTrigger("Hurt");
    }

    IEnumerator Die()
    {
        yield return GameController.Instance.LoadScene(GameController.Instance.currentLevel.name);
        GameController.Instance.playerHealth--;
        GameController.Instance.score = 0;
    }
    /// <summary>
    /// 角色右移
    /// </summary>
    public void MoveRight()
    {
        if (left == true && right == false)
        {
            this.transform.Rotate(0, 180, 0);
            right = true;
            left = false;
            this.forward = Vector2.right;
        }
        playerAnimator.SetBool("Run", true);
        this.transform.Translate(speed * Time.deltaTime, 0, 0);
    }
    /// <summary>
    /// 角色左移
    /// </summary>
    public void MoveLeft()
    {
        if (left == false && right == true)
        {
            transform.Rotate(0, 180, 0);
            right = false;
            left = true;
            this.forward = Vector2.left;
        }
        playerAnimator.SetBool("Run", true);
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
    /// <summary>
    /// 角色上爬
    /// </summary>
    public void ClimbUp()
    {
        playerAnimator.SetBool("Climb", true);
        this.transform.Translate(0, speed * Time.deltaTime, 0);
    }
    /// <summary>
    /// 角色下爬
    /// </summary>
    public void ClimbDown()
    {
        playerAnimator.SetBool("Climb", true);
        this.transform.Translate(0, -speed * Time.deltaTime, 0);
    }
    /// <summary>
    /// 角色攻击
    /// </summary>
    public void PlayerAttack()
    {
        Vector3 dir = this.transform.localPosition;
        if (canAttack == true)
        {
            GameObject go = Instantiate(weapon, weaponPoint.transform.position, weaponPoint.transform.rotation);
            if (forward == Vector2.right)
            {
                go.GetComponent<Rigidbody2D>().AddForce(dir.normalized * force, ForceMode2D.Force);
            }
            if (forward == Vector2.left)
            {
                go.GetComponent<Rigidbody2D>().AddForce(-dir.normalized * force, ForceMode2D.Force);
            }

        }
    }
    /// <summary>
    /// 角色跳跃
    /// </summary>
    public void PlayerJump()
    {
        jumpCount++;
        if (jumpCount <= 2)
        {
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(new Vector2(0, force), ForceMode2D.Force);
            playerAnimator.SetBool("Jump", true);
        }
    }
}
