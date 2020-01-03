using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_background : MonoBehaviour
{
    public int speed = 10;
    public GameObject T;
    public PlayerController Carton;
    public Camera cam;
    public AudioPlayer audio;
    private float defaultX;
    private string[] index = { "Front", "Back" };
    private Vector3[] P = new Vector3[2];
    private int op = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultX = cam.transform.position.x;
        T = this.gameObject;
        P[0] = T.transform.Find(index[0]).position;
        P[1] = T.transform.Find(index[1]).position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 xP = cam.transform.position;
        if (xP.x-P[op].x >= 45.5)
        {
            P[op].x = xP.x + 45.5f;
            op = op ^ 1;
        }
        float times = audio.getAudioSourceTime();
        if (times < 0)
        {
            xP.x = xP.x + Time.deltaTime * speed;
            Carton.setRbodyX(Carton.getRbodyX() + Time.deltaTime * speed);
        }
        else
        {
            xP.x = defaultX + times * speed;
            Carton.setRbodyX(Carton.defaultX + times * speed);
        }
        cam.transform.position = xP;
        for(int i = 0;i < 2; i++)
        {
            T.transform.Find(index[i]).position = P[i];
        }
    }
}
