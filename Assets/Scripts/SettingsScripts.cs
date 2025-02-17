using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScripts : MonoBehaviour
{

    public CanvasGroup SettingsScreen;
    private int UI_Alpha = 0;
    private float alphaSpeed = 10f;
    public int Hided = 1;
    // 退出音效
    public AudioSource ExitSFX;

    // 右侧面板 相关
    public GameObject PCSets;
    public GameObject AudioSets;
    public int CurrentSets = 0;

    // PC设置 相关
    public GameObject FullscreenToggleText;
    public GameObject ResolutionToggleText;

    // 音频设置 相关
    public int VoiceLang = 0;
    public GameObject VoiceLangToggleText;
    public GameObject ObjBubble;
    public GameObject ObjBubbleCN;
    public AudioSource ObjVoice;
    public AudioSource ObjVoiceCN;

    // AudioMixer 相关
    public AudioMixer AudioMixer;
    public Slider BGMSlider;
    public Slider SFXSlider;

    // 单例模式
    private static SettingsScripts _instance;
    public static SettingsScripts Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;

    }

    private void Start()
    {
        SetBGMVolume(BGMSlider.value);
        SetSFXVolume(SFXSlider.value);
    }

    private void Update()
    {
        // 退出设置
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(1)) {
            Hide();
        }
        // 检测全屏开关状态，并显示对应文字
        if (Screen.fullScreen)
        {
            FullscreenToggleText.GetComponent<TMP_Text>().text = ("开");
        }
        else
        {
            FullscreenToggleText.GetComponent<TMP_Text>().text = ("关");
        }
        // 检测当前分辨率，并显示对应文字
        ResolutionToggleText.GetComponent<TMP_Text>().text = (Screen.width+"x"+Screen.height);

        // 检测选中的设置，并改变右侧面板
        if (CurrentSets == 0)
        {
            PCSets.SetActive(true);
            AudioSets.SetActive(false);
        }
        else if (CurrentSets == 1)
        {
            PCSets.SetActive(false);
            AudioSets.SetActive(true);
        }
        else
        {
            CurrentSets = 0;
        }

        // 检测当前语音语言
        if (VoiceLang == 0)
        {
            VoiceLangToggleText.GetComponent<TMP_Text>().text = ("日语");
        }
        else if (VoiceLang == 1)
        {
            VoiceLangToggleText.GetComponent<TMP_Text>().text = ("汉语");
        }
        else
        {
            VoiceLang = 0;
        }

        // 设置界面渐显渐隐
        if (UI_Alpha != SettingsScreen.alpha)
        {
            SettingsScreen.alpha = Mathf.Lerp(SettingsScreen.alpha, UI_Alpha, alphaSpeed * Time.deltaTime);
            if (Mathf.Abs(UI_Alpha - SettingsScreen.alpha) <= 0.01f)
            {
                SettingsScreen.alpha = UI_Alpha;
            }
        }
    }

    //显示面板
    public void OpenSettings()
    {
        UI_Alpha = 1;
        SettingsScreen.blocksRaycasts = true;
        Hided = 0;
    }

    // 隐藏面板
    public void Hide()
    {
        if(Hided == 0)
        {
            ExitSFX.Play();
            UI_Alpha = 0;
            SettingsScreen.alpha = 0;
            SettingsScreen.blocksRaycasts = false;
            Hided = 1;
            SystemDataScripts.Instance.SendMessage("SaveBySerialization");
        }
    }

    // 切换全屏
    public void ToggleFullscreen()
    {
        if (Screen.fullScreen)
        {
            Screen.fullScreen = false;
        }
        else
        {
            Screen.fullScreen = true;
        }
    }

    // 缩小分辨率
    public void ResSmaller()
    {
        if (Screen.width <= 1280)
        {
            if (Screen.fullScreen)
            {
                Screen.SetResolution(1920, 1080, true);
            }
            else
            {
                Screen.SetResolution(1920, 1080, false);
            }
        }
        else if (Screen.width == 1366)
        {
            if (Screen.fullScreen)
            {
                Screen.SetResolution(1280, 720, true);
            }
            else
            {
                Screen.SetResolution(1280, 720, false);
            }
        }
        else if (Screen.width == 1600)
        {
            if (Screen.fullScreen)
            {
                Screen.SetResolution(1366, 768, true);
            }
            else
            {
                Screen.SetResolution(1366, 768, false);
            }
        }
        else if (Screen.width == 1920)
        {
            if (Screen.fullScreen)
            {
                Screen.SetResolution(1600, 900, true);
            }
            else
            {
                Screen.SetResolution(1600, 900, false);
            }
        }
        else if (Screen.width > 1920)
        {
            if (Screen.fullScreen)
            {
                Screen.SetResolution(1920, 1080, true);
            }
            else
            {
                Screen.SetResolution(1920, 1080, false);
            }
        }
    }

    // 增加分辨率
    public void ResBigger()
    {
        if (Screen.width < 1280)
        {
            if (Screen.fullScreen)
            {
                Screen.SetResolution(1280, 720, true);
            }
            else
            {
                Screen.SetResolution(1280, 720, false);
            }
        }
        if (Screen.width == 1280)
        {
            if (Screen.fullScreen)
            {
                Screen.SetResolution(1366, 768, true);
            }
            else
            {
                Screen.SetResolution(1366, 768, false);
            }
        }
        else if (Screen.width == 1366)
        {
            if (Screen.fullScreen)
            {
                Screen.SetResolution(1600, 900, true);
            }
            else
            {
                Screen.SetResolution(1600, 900, false);
            }
        }
        else if (Screen.width == 1600)
        {
            if (Screen.fullScreen)
            {
                Screen.SetResolution(1920, 1080, true);
            }
            else
            {
                Screen.SetResolution(1920, 1080, false);
            }
        }
        else if (Screen.width >= 1920)
        {
            if (Screen.fullScreen)
            {
                Screen.SetResolution(1280, 720, true);
            }
            else
            {
                Screen.SetResolution(1280, 720, false);
            }
        }
    }

    public void VoiceLangToggle()
    {
        if(VoiceLang == 0)
        {
            VoiceLang = 1;
            ObjBubbleCN.SetActive(true);
            ObjVoiceCN.Play();
            Invoke("ObjBubbleHide", 2f);
        }
        else
        {
            VoiceLang = 0;
            ObjBubble.SetActive(true);
            ObjVoice.Play();
            Invoke("ObjBubbleHide", 2f);
        }
    }

    // 隐藏反对气泡
    public void ObjBubbleHide()
    {
        ObjBubble.SetActive(false);
        ObjBubbleCN.SetActive(false);
    }

    // 设置选项卡
    public void SetLeftPanelChange(int num)
    {
        if (num == 1)
        {
            CurrentSets = 1;
        }
        else
        {
            CurrentSets = 0;
        }
    }

    ///<summary>
    ///https://www.cnblogs.com/alanshreck/p/14746347.html
    ///</summary>
    //控制BGM音量
    public void SetBGMVolume(float v)
    {
        if(v == 5)
        {
            AudioMixer.SetFloat("BGM", 0);
        }
        else if(v == 4)
        {
            AudioMixer.SetFloat("BGM", -10);
        }
        else if (v == 3)
        {
            AudioMixer.SetFloat("BGM", -20);
        }
        else if (v == 2)
        {
            AudioMixer.SetFloat("BGM", -30);
        }
        else if (v == 1)
        {
            AudioMixer.SetFloat("BGM", -40);
        }
        else if (v == 0)
        {
            AudioMixer.SetFloat("BGM", -80);
        }
    }

    //控制SFX音量
    public void SetSFXVolume(float v)
    {
        if (v == 5)
        {
            AudioMixer.SetFloat("SFX", 0);
        }
        else if (v == 4)
        {
            AudioMixer.SetFloat("SFX", -10);
        }
        else if (v == 3)
        {
            AudioMixer.SetFloat("SFX", -20);
        }
        else if (v == 2)
        {
            AudioMixer.SetFloat("SFX", -30);
        }
        else if (v == 1)
        {
            AudioMixer.SetFloat("SFX", -40);
        }
        else if (v == 0)
        {
            AudioMixer.SetFloat("SFX", -80);
        }
    }
}
