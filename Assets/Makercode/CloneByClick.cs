using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneByClick : MonoBehaviour
{
    public float x, y, z;
    public GameObject prefab, obj;
    static float timeSpend = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSpend += UnityEngine.Time.deltaTime;
    }

    void OnMouseDown()
    {
        float a = GameObject.Find("Main Camera").transform.localPosition.x;
        float b = GameObject.Find("Main Camera").transform.localPosition.y;
        float c = GameObject.Find("Main Camera").transform.localPosition.z;
        if (Input.GetMouseButtonDown(0) && timeSpend > 0.8)
        {
            obj = Instantiate(prefab) as GameObject;
            obj.transform.position = new Vector3(a + x, y, z);
            timeSpend = 0.0f;
        }
    }
}
