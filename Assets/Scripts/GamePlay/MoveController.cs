using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public int speed = 10;          //移动速度  
    public Camera camera;
    public AudioPlayer audio;      
    public PlayerController carton;
    private GameObject Background;
    private float defaultX;         //相机初始位置
    private float dis = 45.5f;      //地面移动距离
    private string[] index = { "Front", "Back" };
    private Vector3[] P = new Vector3[2];
    private int op = 0;

    private void Start()
    {
        defaultX = camera.transform.position.x;
        Background = this.gameObject;
        P[0] = Background.transform.Find(index[0]).position;
        P[1] = Background.transform.Find(index[1]).position;
    }

    private void MoveFloor(Vector2 xP)
    {
        if (xP.x - P[op].x >= dis)
        {
            P[op].x = xP.x + dis;
            op ^= 1;
        }
        for (int i = 0; i < 2; i++)
        {
            Background.transform.Find(index[i]).position = P[i];
        }
    }

    private void MoveCamera(Vector3 xP)
    {
        float times = audio.getAudioSourceTime();
        if (times < 0)
        {
            xP.x = xP.x + Time.deltaTime * speed;
            carton.setRbodyX(carton.getRbodyX() + Time.deltaTime * speed);
        }
        else
        {
            xP.x = defaultX + times * speed;
            carton.setRbodyX(carton.defaultX + times * speed);
        }
        camera.transform.position = xP;
    }

    private void FixedUpdate()
    {
        MoveFloor(camera.transform.position);
        MoveCamera(camera.transform.position);
    }
}
