using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
//using NAudio;
//using NAudio.Wave;
using System.IO;

[RequireComponent(typeof(AudioSource))]

public class AudioPlayer : MonoBehaviour
{
    //==================组件
    public AudioClip audioClip;
    //public Text audioTime;
    //public Text audioName;
    public Slider audioTimeSlider;
    public AudioSource audioSource;
    public string changefile;
    //====================当前时间/总时间
    private int currentHour;
    private int currentMinute;
    private int currentSecond;
        //===总
    private int clipHour;
    private int clipMinute;
    private int clipSecond;
    private bool flag = false;

    public void Start()
    {
        flag = true;
        changefile = "";
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        //audioName.text = audioClip.name;
        clipHour = (int)audioSource.clip.length / 3600;
        clipMinute = (int)(audioSource.clip.length - clipHour * 3600) / 60;
        clipSecond = (int)(audioSource.clip.length - clipHour * 3600 - clipMinute * 60);
        audioSource.Play();
    }
    void Update()
    {
        //Debug.Log(audioSource.time + " " + audioClip.length);
        
        if (flag)
        {
            if (audioSource.time >= audioClip.length - 0.5f)
            {
                flag = false;
            }else
                UpdateSliderValue();
        }
        if (changefile.Length != 0)
        {
            StartCoroutine(LoadMusic(changefile, "E:\\github\\Client\\Assets\\music"));
            flag = true;
            sstart();
        }
    }

    //=====================随着音乐播放不断更新滑动条
    private void UpdateSliderValue()
    {
        currentHour = (int)audioSource.time / 3600;
        currentMinute = (int)( audioSource.time - currentHour * 3600 ) / 60;
        currentSecond = (int)( audioSource.time - currentHour * 3600 - currentMinute * 60 );
        audioTimeSlider.value = audioSource.time / audioClip.length;
    }
    //================通过滑动条改变音乐时间
    private void SetAudioTimeBySliderValueChange()
    {
        audioSource.time = audioTimeSlider.value * audioSource.clip.length;
    }

    public float getAudioSourceTime()
    {
        if (flag)
        {
            return audioSource.time;
        }
        return -1;
    }

    private void AudioPause()
    {
        audioSource.Pause();
    }
    
    private void AudioPlay()
    {
        audioSource.Play();
    }

    private void AudioStop()
    {
        audioSource.Stop();
    }

    void sstart()
    {
        changefile = "";
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        //audioName.text = audioClip.name;
        clipHour = (int)audioSource.clip.length / 3600;
        clipMinute = (int)(audioSource.clip.length - clipHour * 3600) / 60;
        clipSecond = (int)(audioSource.clip.length - clipHour * 3600 - clipMinute * 60);
        audioSource.Play();
        //================= 事件监听
       
    }

    private IEnumerator LoadMusic(string filepath, string savepath)//filepath:mp3的路径，savepath：转换成wav的路径
    {
        var www = new WWW("file://" + filepath);
        yield return www;
        audioClip = www.GetAudioClip();
        Debug.Log(audioClip);
    }
}