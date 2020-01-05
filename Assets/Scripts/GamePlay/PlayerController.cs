using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//角色动画逻辑与游戏逻辑
public class PlayerController : MonoBehaviour
{
    public float defaultX;      //默认初始位置
    public float lowPositionY;  //最低位置
    public float highPositionY; //最高位置
    public float floatingTime;  //跳跃时长
    public float atkingTime;    //攻击时长
    public float downTime;      //下蹲时长
    
    private Vector2 Position;
    private Rigidbody2D rbody;      //刚体
    private SpriteRenderer sprite;  //精灵体
    private Animator playerAnimator;//动画控制器

    public enum STATUS { RUN, ATTACK, JUMP, DOWN, DEAD }
    public STATUS kind = STATUS.RUN;   //行动状态(动画控制)
    public float floatingTimer = 0;    //跳跃计时器
    public float atkingTimer = 0;      //攻击计时器
    public float downTimer = 0;        //下蹲计时器

    private bool damaged = false;       //是否受到伤害
    private Color flashColour = new Color(1, 0, 0, 1);
    private Color Write = new Color(1, 1, 1, 1);
    private float flashSpeed = 5.0f;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        rbody.MovePosition(new Vector2(defaultX, lowPositionY));
    }

    public Vector2 getPosition()
    {
        return rbody.position;
    }

    public void setRbodyX(float x)
    {
        if (kind == STATUS.JUMP)
        {
            Position.Set(x, highPositionY);
        }
        else
        {
            Position.Set(x, lowPositionY);
        }
        rbody.MovePosition(Position);
    }

    public float getRbodyX()
    {
        return rbody.position.x;
    }

    private void CloseAllStatus()
    {
        kind = STATUS.RUN;
        atkingTimer = downTimer = floatingTimer = 0;
    }

    private void Control()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W");
            CloseAllStatus();
            kind = STATUS.JUMP;
            floatingTimer = floatingTime;
            Position.Set(rbody.position.x, highPositionY);
            rbody.MovePosition(Position);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("J");
            CloseAllStatus();
            kind = STATUS.ATTACK;
            atkingTimer = atkingTime;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S");
            CloseAllStatus();
            kind = STATUS.DOWN;
            downTimer = downTime;
            Position.Set(rbody.position.x, lowPositionY);
            rbody.MovePosition(Position);
        }
        
    }

    private void ChangeToDown()
    {
        if (kind == STATUS.DOWN)
        {
            if (downTimer <= 0)
                kind = STATUS.RUN;
            else
                kind = STATUS.DOWN;
        }
        else
        {
            downTimer = 0;
        }
    }
    private void ChangeToAttack()
    {
        if (kind == STATUS.ATTACK)
        {
            if (atkingTimer <= 0)
                kind = STATUS.RUN;
            else
                kind = STATUS.ATTACK;
        }
        else
        {
            atkingTimer = 0;
        }

    }
    private void ChangeToJump()
    {
        if (kind == STATUS.JUMP)
        {
            if (floatingTimer <= 0)
            {
                kind = STATUS.RUN;
                Position.Set(rbody.position.x, lowPositionY);
                rbody.MovePosition(Position);
            }
            else
                kind = STATUS.JUMP;
        }
        else
        {
            floatingTimer = 0;
        }
    }
    private void Change()
    {
        ChangeToAttack();
        ChangeToDown();
        ChangeToJump();
        
        if (kind == STATUS.ATTACK)
            atkingTimer -= Time.deltaTime;
        if (kind == STATUS.DOWN)
            downTimer -= Time.deltaTime;
        if(kind == STATUS.JUMP)
            floatingTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Control();
        Change();
        playerAnimator.SetInteger("kind", (int)kind);
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