using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makerui : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public TweenColor color;
    public void exit()
    {
        color.PlayForward();
    }
    public TweenColor mengban;
    public TweenScale loding;
    public void back()
    {
        mengban.PlayReverse();
        loding.PlayReverse();
    }
}
