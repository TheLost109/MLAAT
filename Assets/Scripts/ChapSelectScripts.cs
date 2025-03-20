using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChapSelectScripts : MonoBehaviour
{
    public GameObject TSBG;
    public AudioSource ExitSFX;
    public GameObject TSBeginObj;
    public GameObject TSBeginLogo;
    public GameObject LoadScreen;
    public GameObject LoadPanel;
    public GameObject LoadSuccess;
    public GameObject TransitionQ;
    public GameObject TransitionE;
    public GameObject TSPanel;
    public GameObject MLIPanel;
    public GameObject EoJPanel;
    public GameObject MuseumPanel;
    // 音频相关
    public AudioWithIntro TSMusic;
    public AudioWithIntro MLIMusic;
    public AudioWithIntro EoJMusic;
    public AudioWithIntro MuseumMusicMelody;
    public AudioWithIntro MuseumMusicRhythm;
    public MusicFade MuseumMusicMelodyClipIntro;
    public MusicFade MuseumMusicMelodyClipLoop;
    public AudioSource CursorSFX;
    public AudioSource OpenMenuSFX;
    // 动作放映室
    public GameObject AnimStudioObj;
    public GameObject AnimStudioOverlay;
    // 底部导航
    public GameObject BottomNav;
    public GameObject AnimStudioNav;

    private int CurrentGame = 0;
    private int OnTransition = 0;

    private int InAlertPage = 0;
    private int InAnimStudio = 0;
    private int EnteringGame = 0;

    private void Awake()
    {

    }

    private void Start()
    {
        //TSBG.SetActive(true);
        DataCarrier.Instance.InGame = false;
        LoadScreen.GetComponent<Graphic>().CrossFadeAlpha(0, 0, false);
        LoadSuccess.GetComponent<Graphic>().CrossFadeAlpha(0, 0, false);
    }

    private void Update()
    {
        if(SettingsScripts.Instance.Hided == 0 || InAnimStudio == 1 || EnteringGame == 1)
        {
            BottomNav.SetActive(false);
        }
        // 调整返回操作优先级
        if (SettingsScripts.Instance.Hided == 1 && InAlertPage == 0 && EnteringGame == 0)
        {
            if (InAnimStudio == 0)
            {
                BottomNav.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
                {
                    BacktoMenu();
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    SettingsScripts.Instance.SendMessage("OpenSettings");
                    OpenMenuSFX.Play();
                }
                // 切换游戏
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SwitchGame();
                }
            }
            if (InAnimStudio == 1)
            {
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
                {
                    ExitAnimStudio();
                }
            }
        }
    }
    public void BeginTS()
    {
        EnteringGame = 1;
        TSBeginObj.SetActive(true);
        Invoke("ShowTSLogo", 1f);
        Invoke("LoadTS", 7f);
    }
    public void ShowTSLogo()
    {
        TSBeginLogo.SetActive(true);
    }
    public void LoadTS()
    {
        DataCarrier.Instance.GameID = 0;
        DataCarrier.Instance.ChapterID = 0;
        DataCarrier.Instance.AreaID = 0;
        DataCarrier.Instance.dialogIndex = 0;
        SceneManager.LoadScene("MainGame");
    }
    public void BacktoMenu()
    {
        ExitSFX.Play();
        SceneManager.LoadScene("Menu");
    }
    public void ShowLoadScreen()
    {
        LoadScreen.SetActive(true);
        LoadScreen.GetComponent<Graphic>().CrossFadeAlpha(.99f, .25f, false);
        Invoke("ShowLoadPanel", .35f);
        InAlertPage = 1;
    }

    private void ShowLoadPanel()
    {
        LoadPanel.SetActive(true);
    }

    public void OnCancelLoad()
    {
        LoadScreen.SetActive(false);
        LoadScreen.GetComponent<Graphic>().CrossFadeAlpha(0f, 0f, false);
        LoadPanel.SetActive(false);
        InAlertPage = 0;
    }

    public void SwitchGame()
    {
        if (OnTransition == 0)
        {
            if (CurrentGame == 0)
            {
                OnTransition = 1;
                CurrentGame = 1;
                TransitionE.SetActive(true);
                CursorSFX.Play();
                TSMusic.Stop();
                MLIMusic.Play();
                Invoke("CloseTS", .25f);
                Invoke("OpenMLI", .75f);
                Invoke("TransEEnd", 1f);
            }
            else if (CurrentGame == 1)
            {
                OnTransition = 1;
                CurrentGame = 2;
                TransitionE.SetActive(true);
                CursorSFX.Play();
                MLIMusic.Stop();
                EoJMusic.Play();
                Invoke("CloseMLI", .25f);
                Invoke("OpenEoJ", .75f);
                Invoke("TransEEnd", 1f);
            }
            else if (CurrentGame == 2)
            {
                OnTransition = 1;
                CurrentGame = 3;
                TransitionE.SetActive(true);
                CursorSFX.Play();
                EoJMusic.Stop();
                MuseumMusicMelody.Play();
                MuseumMusicRhythm.Play();
                Invoke("CloseEoJ", .25f);
                Invoke("OpenMuseum", .75f);
                Invoke("TransEEnd", 1f);
            }
            else if (CurrentGame == 3)
            {
                OnTransition = 1;
                CurrentGame = 0;
                TransitionE.SetActive(true);
                CursorSFX.Play();
                MuseumMusicMelody.Stop();
                MuseumMusicRhythm.Stop();
                TSMusic.Play();
                Invoke("CloseMuseum", .25f);
                Invoke("OpenTS", .75f);
                Invoke("TransEEnd", 1f);
            }
        }
    }
    private void CloseTS()
    {
        TSPanel.SetActive(false);
    }
    private void OpenTS()
    {
        TSPanel.SetActive(true);
    }
    private void CloseMLI()
    {
        MLIPanel.SetActive(false);
    }
    private void OpenMLI()
    {
        MLIPanel.SetActive(true);
    }
    private void CloseEoJ()
    {
        EoJPanel.SetActive(false);
    }
    private void OpenEoJ()
    {
        EoJPanel.SetActive(true);
    }
    private void CloseMuseum()
    {
        MuseumPanel.SetActive(false);
    }
    private void OpenMuseum()
    {
        MuseumPanel.SetActive(true);
    }
    private void TransQEnd()
    {
        TransitionQ.SetActive(false);
        OnTransition = 0;
    }
    private void TransEEnd()
    {
        TransitionE.SetActive(false);
        OnTransition = 0;
    }

    public void GoAnimStudio()
    {
        MuseumPanel.SetActive(false);
        AnimStudioObj.SetActive(true);
        AnimStudioOverlay.SetActive(true);
        AnimStudioNav.SetActive(true);
        MuseumMusicMelodyClipIntro.FadeOut();
        MuseumMusicMelodyClipLoop.FadeOut();
        // 改变底部导航
        InAnimStudio = 1;
    }
    public void ExitAnimStudio()
    {
        MuseumPanel.SetActive(true);
        AnimStudioObj.SetActive(false);
        AnimStudioOverlay.SetActive(false);
        AnimStudioNav.SetActive(false);
        MuseumMusicMelodyClipIntro.FadeIn();
        MuseumMusicMelodyClipLoop.FadeIn();
        ExitSFX.Play();
        // 改变底部导航
        InAnimStudio = 0;
    }
}
