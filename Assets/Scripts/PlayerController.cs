using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
// 控制角色移动 生命
//


public class PlayerController : MonoBehaviour
{
    //private float speed = 3f;//移动速度
    //private float vspeed = 0;//垂直方向的速度
    //private float gr = 0.6f;//重力加速度
    Rigidbody2D rbody;
    BoxCollider2D mybody;
    SpriteRenderer sprite;

    public Camera cam;
    public float defaultX;//默认初始位置
    public float lowPositionY;//最低位置
    public float highPositionY;//最高位置
    //public float transitionPositionY;
    private Vector2 lowPosition;
    private Vector2 highPosition;
    private Vector2 Position;

    enum STATUS { RUN, ATTACK, JUMP, DOWN, DEAD }
    public int kind = (int)STATUS.RUN;//行动状态

    private bool jump = false;
    private bool down = false;
    private bool atking = false;//是否正在攻击

    private bool sp_atking = false;//连续攻击
    private int sp_atklat = 0;//可以持续连续攻击的时间

    private bool damaged = false;//是否受到伤害
    private Color flashColour = new Color(1, 0, 0, 1);
    private Color Write = new Color(1, 1, 1, 1);
    private float flashSpeed = 5.0f;

    public float floatingTime;
    public float atkingTime;
    public float downTime;
    private float floatingTimer = 0;
    private float atkingTimer = 0;
    private float downTimer = 0;

    private Animator playerAnimator = null;// 动画控制器

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        mybody = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        //Debug.Log(rbody.name);
        playerAnimator = GetComponent<Animator>();
        lowPosition = new Vector2(defaultX, lowPositionY);
        highPosition = new Vector2(defaultX, highPositionY);
        //transitionPosition = new Vector2(defaultX, transitionPositionY);
        rbody.MovePosition(new Vector2(defaultX, lowPositionY));
    }
    //public float goodAreaUpperBound,goodAreaLowerBound,
    //             perfectAreaUpperBound,perfectAreaLowerBound;

    //private void gradeJudge(bool state,Collider2D other)
    //{
    //    float roleX, otherX, offsetX;
    //        if (true)
    //        {
    //        Debug.Log("in");
    //            roleX = transform.position.x;
    //            otherX = other.transform.position.x;
    //            offsetX = otherX - roleX;
    //        Debug.Log("rolex :" + roleX + "  otherX:" + otherX + "  offsetX:" + offsetX);
    //            if (offsetX > goodAreaLowerBound && offsetX <= goodAreaUpperBound)
    //            {
    //                Debug.Log("good");
    //                other.GetComponent<Collider2D>().enabled = false;
    //            }
    //            if (offsetX > perfectAreaLowerBound && offsetX <= perfectAreaUpperBound)
    //            {
    //                Debug.Log("perfect");
    //                other.GetComponent<Collider2D>().enabled = false;
    //            }
    //        }   
    //}
    private bool listenKey(KeyCode x)
    {
        if (Input.GetKeyDown(x))
        {
            return true;
        }
        else
            return false;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "downObstacleGoodArea")
        {
            if (listenKey(KeyCode.S))
            {
                Debug.Log("good");
                other.gameObject.GetComponent<Collider2D>().enabled = false;
                other.gameObject.transform.parent.gameObject.transform.Find("perfectCollider").GetComponent<Collider2D>().enabled = false;
            }
        }
        if (other.tag == "downObstaclePerfectArea")
        {
            if (listenKey(KeyCode.S))
            {
                Debug.Log("perfect");
                other.gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
        if (other.tag == "jumpObstacleGoodArea")
        {
            if (listenKey(KeyCode.W))
            {
                Debug.Log("good");
                other.gameObject.GetComponent<Collider2D>().enabled = false;
                other.gameObject.transform.parent.gameObject.transform.Find("perfectCollider").GetComponent<Collider2D>().enabled = false;
            }
        }
        if (other.tag == "jumpObstaclePerfectArea")
        {
            if (listenKey(KeyCode.W))
            {
                Debug.Log("perfect");
                other.gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
        if (other.tag == "attackEnemyGoodArea")
        {
            if (listenKey(KeyCode.J))
            {
                Debug.Log("good");
                other.gameObject.GetComponent<Collider2D>().enabled = false;
                other.gameObject.transform.parent.gameObject.transform.Find("perfectCollider").GetComponent<Collider2D>().enabled = false;
            }
        }
        if (other.tag == "attackEnemyPerfectArea")
        {
            if (listenKey(KeyCode.J))
            {
                Debug.Log("perfect");
                other.gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }


        ////Debug.Log("onTriggrtEnter tag:"+other.tag);
        //if (other.tag == "downObstacle")
        //{
        //    gradeJudge(down, other);
        //}
        //if (other.tag == "jumpObstacle")
        //{
        //    gradeJudge(jump, other);
        //}
        //if (other.tag == "Enemy")
        //{
        //    gradeJudge(atking, other);
        //}

        //if (other.tag == "Fire")
        //{
        //    damaged = true;
        //    GameController.instance.UpdataAndDisplayHp(-1);
        //}
        //else if (other.tag == "Food")
        //{
        //    GameController.instance.UpdataAndDisplaySc(10);
        //}
        //else if (other.tag == "Enemy")
        //{
        //    if (atking == false && sp_atking == false)
        //    {
        //        damaged = true;
        //        GameController.instance.UpdataAndDisplayHp(-1);
        //    }
        //}
        //else if (other.tag == "Goods")
        //{
        //    if (atking || sp_atking)
        //    {
        //        GameController.instance.UpdataAndDisplaySc(10);
        //    }


    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "downObstaclePerfectArea" || other.tag== "jumpObstaclePerfectArea" || other.tag == "attackEnemyPerfectArea")
        {
            damaged = true;
            //GameController.instance.UpdataAndDisplayHp(-1); 
            Debug.Log("HP--");
        }

    }
    public void setRbodyX(float x)
    {
        if (jump)
        {
            Position.Set(x, highPositionY);
        }
        else
        {
            Position.Set(x, lowPositionY);
        }
        rbody.MovePosition(Position);
        //Debug.Log(rbody.position.x);
    }
    public float getRbodyX()
    {
        return rbody.position.x;
    }
    // Update is called once per frame
    private void CloseAllStatus()
    {
        jump = false;
        atking = false;
        down = false;
        atkingTimer = downTimer = 0;
    }
    private void Control()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //Debug.Log("W");
            CloseAllStatus();
            jump = true;
            floatingTimer = floatingTime;
            highPosition.Set(rbody.position.x, highPositionY);
            rbody.MovePosition(highPosition);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            //Debug.Log("J");
            CloseAllStatus();
            atking = true;
            atkingTimer = atkingTime;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Debug.Log("S");
            CloseAllStatus();
            down = true;
            downTimer = downTime;
            lowPosition.Set(rbody.position.x, lowPositionY);
            rbody.MovePosition(lowPosition);
        }
        
    }
    private void ChangeToDown()
    {
        if (downTimer <= 0)
            down = false;
        if (down)
        {
            kind = (int)STATUS.DOWN;
            //Vector2 PP = new Vector2(0.0001f, 0.0001f);
            //mybody.size = PP;
        }
        else
        {
            downTimer = 0;
            //Vector2 PP = new Vector2(1, 1);
            //mybody.size = PP;
        }
    }
    private void ChangeToAttack()
    {
        if (atkingTimer <= 0)
            atking = false;
        if (atking)
        {
            kind = (int)STATUS.ATTACK;
        }
        else
        {
            atkingTimer = 0;
        }

    }
    private void ChangeToJump()
    {
        if (floatingTimer <= 0)
        {
            jump = false;
            lowPosition.Set(rbody.position.x, lowPositionY);
            rbody.MovePosition(lowPosition);
        }
        if (jump)
        {
            kind = (int)STATUS.JUMP;
        }
        else
        {
            floatingTimer = 0;
        }
    }
    private void Change()
    {
        kind = 0;
        ChangeToAttack();
        ChangeToDown();
        ChangeToJump();
        
        if (atking)
            atkingTimer -= Time.deltaTime;
        if (down)
            downTimer -= Time.deltaTime;
        if(jump)
            floatingTimer -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        Control();
        Change();
        playerAnimator.SetInteger("kind", kind);
    }
    void Update()
    {
        if (damaged)
        {
            sprite.color = flashColour;
        }
        else
        {
            sprite.color = Write;
        }
        damaged = false;
    }
}