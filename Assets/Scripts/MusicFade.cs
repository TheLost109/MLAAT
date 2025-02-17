using UnityEngine;
using System;

//<summary>
//https://www.codeleading.com/article/22115502932/
//</summary>
public class MusicFade : MonoBehaviour
{
    // 单例模式
    private static MusicFade _instance;
    public static MusicFade Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }
    /*
     * 挂载在audiosource类型的gameobj上，使用时调用objname.fadeMusic(volume,time)
     */
    // Start is called before the first frame update
    [SerializeField] private AudioSource Music;
    [SerializeField] private float volumeDelta;
    [SerializeField] private float targetvolume;
    [SerializeField] private bool isfading;
    [SerializeField] private float durtime = 4;
    void Start()
    {
        Music = GetComponent<AudioSource>();
        volumeDelta = 0;
        //volDecrease = false;
        isfading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isfading) return;
        if (Math.Abs(Music.volume - targetvolume) >= Math.Abs(volumeDelta))
        {
            Music.volume += (float)volumeDelta;
            //Debug.Log("fading...");
        }
        else
        {
            Music.volume = targetvolume;
            isfading = false;
        }
    }

    public void FadeMusic(float targetVolume/*0-1*/, float durtime/*seconds*/)
    {
        Debug.Log("Music fade set target = " + targetVolume);
        targetvolume = targetVolume;
        if (Music == null) Music = GetComponent<AudioSource>();
        float timedelta = durtime / Time.deltaTime;
        if (timedelta > 0)
            volumeDelta = (targetVolume - Music.volume) / timedelta;
        else
        {
            volumeDelta = (targetVolume - Music.volume);
        }

        isfading = true;
    }

    public void FadeOut()
    {
        FadeMusic(0, durtime);
    }

    public void FadeIn()
    {
        FadeMusic(1, durtime);
    }

}