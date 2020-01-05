using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMessage : MonoBehaviour
{
    //public GameObject gameLoad;
    public string url;
    public string name;

    public LoadMessage(string url, string name)
    {
        this.url = url;
        this.name = name;
    }

    public LoadMessage()
    {
    }

    private void Start()
    {
        url = @"./song";
        name = "runner";
        GameObject.Find("GameLoad").GetComponent<LoadingMap>().enabled = true;
    }
}
