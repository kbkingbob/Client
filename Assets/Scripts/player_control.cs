using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
// 控制角色移动 生命
//

public class player_control : MonoBehaviour
{
    private float speed = 3f;//移动速度
    private float vspeed = 0;//垂直方向的速度
    private float gr = 0.6f;//重力加速度
    Rigidbody2D rbody;
    BoxCollider2D mybody;
    SpriteRenderer sprite;
    public int kind = 0;//行动状态
    /*
     0 移动
     1 攻击
     2 跳跃
     3 下蹲
     4 死亡
         */
    private bool jump = false;
    private int downlast = 0;
    private const int downlasttime = 60;
    private const int top_jump_dis = 15;
    private bool can_atk = true;
    private bool down = false;
    private bool atking = false;//是否正在攻击
    private int atklast = 0;//剩余攻击时间
    private const int atklasttime = 60;

    private bool sp_atking = false;//连续攻击
    private int sp_atklat = 0;//可以持续连续攻击的时间

    private bool damaged = false;//是否受到伤害
    private Color flashColour = new Color(1, 0, 0, 1);
    private Color Write = new Color(1, 1, 1, 1);
    private float flashSpeed = 5.0f;


    private Animator playerAnimator = null;// 动画控制器
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        mybody = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    public bool is_atking()
    {
        return sp_atking | atking;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("扣血");
        if ( other.tag=="Fire" )
        {
            // Debug.Log("扣血");
            //GameController.instance.Now_hurt();
            damaged = true;
            GameController.instance.UpdataAndDisplayHp(-1);
        }
        else if ( other.tag=="Food" )
        {
          //  Debug.Log("吃到了苹果");
            GameController.instance.UpdataAndDisplaySc(10);
        }
        else if ( other.tag=="Enemy" )
        {
            if ( atking==false && sp_atking==false )
            {
               // GameController.instance.Now_hurt();
                damaged = true;
                GameController.instance.UpdataAndDisplayHp(-1);
            }

        }
        else if ( other.tag=="Goods" )
        {
            if ( atking || sp_atking )
            {
                GameController.instance.UpdataAndDisplaySc(10);
            }

        }
    }
    // Update is called once per frame
    void Control()
    {
        Vector2 position = rbody.position;
        if (Input.GetButtonDown("Jump"))
        {
            if (jump==false && position.y<=-2.0f )
            {
                vspeed = -top_jump_dis;
                jump = true;
            }
        }
        if ( Input.GetButtonDown("Atk") && can_atk==true )
        {
            can_atk = false;
            atking = true;
            atklast = atklasttime;
        }
        if (Input.GetButtonDown("Down"))
        {
            down = true;
            downlast = downlasttime;
            vspeed = 60;
        }

    }
    void Change()
    {
        //碰撞盒缩小
        Vector2 position = rbody.position;
        if (down == true)
        {
            Vector2 PP = new Vector2(0.0001f, 0.0001f);
            mybody.size = PP;
        }
        else
        {
            Vector2 PP = new Vector2(1, 1);
            mybody.size = PP;
        }

        if (atklast == 0)
        {
            can_atk = true;
            atking = false;
        }
        if ( downlast==0 )
        {
            down = false;

        }
        if (atklast > 0)
            atklast--;
        if (downlast > 0)
            downlast--;
        if (position.y <= -2.0f)
            jump = false;//可以跳跃
        else if (down == false)
            jump = true;
        kind = 0;
        if (jump == true)
            kind = 2;
        if (atking == true)
            kind = 1;
        if (down == true)
            kind = 3;
        if ( position.y>-2.0f && kind==3 )
        {
            if (atking)
                kind = 1;
            else
                kind = 2;
        }

    }
    void Update()
    {
        playerAnimator = GetComponent<Animator>();
        Vector2 position = rbody.position;
        Control();
        Change();
        
        int sptop = 60;
        vspeed = Mathf.Clamp(vspeed+gr,-sptop,sptop);
        position.y -= vspeed * Time.deltaTime;
        if (position.y <= -2.0f)
            position.y = -2.0f;
        rbody.MovePosition(position);
        if ( damaged )
        {
            sprite.color = flashColour;
        }
        else
        {
            //sprite.color = Color.Lerp(sprite.color, Color.clear, flashSpeed * Time.deltaTime);
            sprite.color = Write;
        }
        damaged = false;
       
        playerAnimator.SetInteger("kind", kind);
    }
}