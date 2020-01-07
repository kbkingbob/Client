using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

class LoadingMap : MonoBehaviour
{
    public float planeDefualY;          //飞机高度
    public float enemyDefualY;          //敌人高度
    public float roadBlockDefualY;      //路障高度
    public GameObject obj;
    public GameObject plane;
    public GameObject enemy;
    public GameObject roadBlock;
    public PlayerController player;
    public AudioPlayer audio;
    public LoadMessage loadMessage;

    private StreamReader map;
    private string cur;
    private string[] split;

    private void Start()
    {
        string url = loadMessage.url;
        string name = loadMessage.name;
        audio.changefile = Path.GetFullPath(url + "/" + name + ".wav");
        map = new StreamReader(url+"/"+name+".map");
        cur = map.ReadLine();
        while (cur != null)
        {
            split = cur.Split(new char[] { ' ', '\n' });
            int type = int.Parse(split[0]);
            float x = float.Parse(split[1]);
            float y = 0;
            float z = 0;
            if (x - player.getPosition().x > 30f) break;
            if (type == 1)
            {
                obj = Instantiate(roadBlock) as GameObject;
                y = roadBlockDefualY;
            }
            else if (type == 2)
            {
                obj = Instantiate(plane) as GameObject;
                y = planeDefualY;
            }
            else if (type == 3)
            {
                obj = Instantiate(enemy) as GameObject;
                y = enemyDefualY;
            }
            obj.transform.position = new Vector3(x, y, z);
            //Destroy(obj, 5);
            cur = map.ReadLine();
        }
        GameObject.Find("Panel").GetComponent<AudioPlayer>().enabled = true;
    }

    private void FixedUpdate()
    {
        if (cur == null) return;
        int type = int.Parse(split[0]);
        float x = float.Parse(split[1]);
        float y = 0;
        float z = 0;
        if (x - player.getPosition().x > 30f) return;
        if (type == 1)
        {
            obj = Instantiate(roadBlock) as GameObject;
            y = roadBlockDefualY;
        }
        else if (type == 2)
        {
            obj = Instantiate(plane) as GameObject;
            y = planeDefualY;
        }
        else if (type == 3)
        {
            obj = Instantiate(enemy) as GameObject;
            y = enemyDefualY;
        }
        obj.transform.position = new Vector3(x, y, z);
        //Destroy(obj, 5);
        cur = map.ReadLine();
        //Debug.Log(cur);
        if (cur != null)
            split = cur.Split(new char[] { ' ', '\n' });
        else
            map.Close();
    }
}
