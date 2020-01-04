using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

class LoadingMap : MonoBehaviour
{
    public float planeDefualY;
    public float enemyDefualY;
    public float roadBlockDefualY;
    public GameObject obj;
    public GameObject plane;
    public GameObject enemy;
    public GameObject roadBlock;
    public LoadMessage loadMessage;
    public AudioPlayer audio;
    private void Start()
    {
        string url = loadMessage.url;
        string name = loadMessage.name;
        audio.changefile = Path.GetFullPath(url + "/" + name + ".wav");
        StreamReader map = new StreamReader(url+"/"+name+".map");
        string str = map.ReadLine();
        while (str != null)
        {
            string[] split = str.Split(new char[] { ' ', '\n' });
            int type = int.Parse(split[0]);
            float x = float.Parse(split[1]);
            float y = 0;
            float z = 0;
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
            str = map.ReadLine();
        }
        map.Close();
        GameObject.Find("Panel").GetComponent<AudioPlayer>().enabled = true;
    }
}
