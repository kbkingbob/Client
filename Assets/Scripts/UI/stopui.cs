using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stopui : MonoBehaviour
{
    public TweenColor change;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject exit;
    public TweenScale cancel;
    public TweenColor mengban;
    public GameObject start;
    public void Stoppanel()
    {
        //        exit.SetActive(true);
        cancel.PlayForward();
        mengban.PlayForward();
        exit.SetActive(false);
        start.SetActive(true);
        //.enabled(ture);
        //GameObject.Find("exit").GetComponent<Animator>().enabled = true;
    }
    public void Stophide()
    {
        mengban.PlayReverse();
        cancel.PlayReverse();
        exit.SetActive(true);
        start.SetActive(false);
    }
    public void change_scene()
    {
        SceneManager.LoadScene("final");
    }
    public void skip()
    {
        change.PlayForward();
    }
}
