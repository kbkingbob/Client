using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class End : MonoBehaviour
{
    GameObject[] sno1;
    string txt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Button()
    {
        StreamWriter F = new StreamWriter("names.txt");
        sno1 = GameObject.FindGameObjectsWithTag("Block");
        foreach(GameObject item in sno1)
        {
            int type = -1;
            if (item.name == "New Sprite (1)(Clone)") type = 1;
            else if (item.name == "New Sprite (2)(Clone)") type = 2;
            else if (item.name == "Enemy(Clone)") type = 3;
            float a = item.transform.localPosition.x;
            float b = item.transform.localPosition.y;
            float c = item.transform.localPosition.z;
            string str = type.ToString() + " " + a.ToString() + " " + b.ToString() + " " + c.ToString();
            if (type != -1)txt += str + "\n";
         }
        F.Write(txt);
        F.Close();
    }
}
