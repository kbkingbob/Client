using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finalui : MonoBehaviour
{
    // Start is called before the first frame update
    public UILabel ul;
    public TweenColor change;
    GameObject t;
    void Start()
    {
        t = GameObject.Find("ssscore");
        ul.text = t.GetComponent<Text>().text;
        Destroy(t);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void skip()
    {
        change.PlayForward();
    }
}
