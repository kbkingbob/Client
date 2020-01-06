using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
//using NAudio;
//using NAudio.Wave;
using System.IO;
using System.Threading;

[RequireComponent(typeof(AudioSource))]

public class AudioPlayer : MonoBehaviour
{
    public AudioClip audioClip;
    public Slider audioTimeSlider;
    public AudioSource audioSource;
    public string changefile;       //切歌歌曲路径

    private int currentHour;
    private int currentMinute;
    private int currentSecond;
    private int clipHour;
    private int clipMinute;
    private int clipSecond;
    private bool flag = false;
    private bool running = false;
    int cnt = 0;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        clipHour = (int)audioSource.clip.length / 3600;
        clipMinute = (int)(audioSource.clip.length - clipHour * 3600) / 60;
        clipSecond = (int)(audioSource.clip.length - clipHour * 3600 - clipMinute * 60);

        StartCoroutine(LoadMusic(changefile));
        clipHour = (int)audioSource.clip.length / 3600;
        clipMinute = (int)(audioSource.clip.length - clipHour * 3600) / 60;
        clipSecond = (int)(audioSource.clip.length - clipHour * 3600 - clipMinute * 60);
        changefile = "";
        
    }

    void FixedUpdate()
    {
        if (!running)
        {
            AudioPlay();
            return;
        }
        if (flag)
        {
            if (audioSource.time >= audioClip.length - 0.5f)
            {
                flag = false;
            }
            else
                UpdateSliderValue();
        }
    }

    //随着音乐播放不断更新滑动条
    private void UpdateSliderValue()
    {
        currentHour = (int)audioSource.time / 3600;
        currentMinute = (int)( audioSource.time - currentHour * 3600 ) / 60;
        currentSecond = (int)( audioSource.time - currentHour * 3600 - currentMinute * 60 );
        audioTimeSlider.value = audioSource.time / audioClip.length;
    }

    public float getAudioSourceTime()
    {
        if (!running) return 0;
        if (flag)
        {
            return audioSource.time;
        }
        return -1;
    }

    public void AudioPause()
    {
        audioSource.Pause();
    }
    
    public void AudioPlay()
    {
        running = true;
        flag = true;
        audioSource.Play();
    }

    public void AudioStop()
    {
        audioSource.Stop();
    }

    //加载音乐
    private IEnumerator LoadMusic(string filepath)
    {
        filepath = "file:///" + filepath;
        Debug.Log(filepath);
        using (var uwr = UnityWebRequestMultimedia.GetAudioClip(filepath, AudioType.WAV))
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError)
            {
                Debug.LogError(uwr.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(uwr);
                Debug.Log(clip);
                audioClip = clip;
                audioSource.clip = clip;
            }
        }
    }
}