using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_background : MonoBehaviour
{
    public int speed = 10;
    public GameObject T;
    public Camera cam;
    public AudioPlayer audio;
    private string[] index = { "Front", "Back" };
    private Vector3[] P = new Vector3[2];
    private int op = 0;

    // Start is called before the first frame update
    void Start()
    {
        T = this.gameObject;
        P[0] = T.transform.Find(index[0]).position;
        P[1] = T.transform.Find(index[1]).position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 xP = cam.transform.position;
        if (xP.x-P[op].x >= 45.5)
        {
            P[op].x = xP.x + 45.5f;
            op = op ^ 1;
        }
        float times = audio.getAudioSourceTime() * speed;
        if (times < 0) times = xP.x + Time.deltaTime * speed;
        xP.x = times;
        Debug.Log(xP.x);
        cam.transform.position = xP;
        for(int i = 0;i < 2; i++)
        {
            T.transform.Find(index[i]).position = P[i];
        }
    }
}
