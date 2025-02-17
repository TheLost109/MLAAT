using UnityEngine;
using UnityEngine.UI;
public class TSController001 : MonoBehaviour
{
    public TextAsset CourtRecordsFile;
    public TextAsset DialogFile0;
    public TextAsset DialogFile1;
    public GameObject dialogBox;
    public GameObject NameTag;
    public GameObject SpeechText;
    public GameObject NewAreaText;
    public GameObject ItemHolder;

    // 底部按钮
    public GameObject SettingsButton;

    // Area 0 开幕
    public AudioSource IntroMusic;

    // Area 1 事务所
    public GameObject OfficeBG;
    public AudioSource RingtonePW;
    public AudioSource PickUpSFX;
    public AudioSource ItemPopSFX;

    public int AreaID = 0;

    private void Awake()
    {

    }

    private void Start()
    {
        if (GameBaseController.Instance.GameID == 0 && GameBaseController.Instance.ChapterID == 0)
        {
            CourtRecords.Instance.SendMessage("ReadRecords", CourtRecordsFile);
            if(GameBaseController.Instance.AreaID == 0)
            {
                GameBaseController.Instance.SendMessage("ReadText", DialogFile0);
            }
            else if(GameBaseController.Instance.AreaID == 1)
            {
                GameBaseController.Instance.SendMessage("ReadText", DialogFile1);
                if (DataCarrier.Instance.InGame)
                {
                    GameBaseController.Instance.SendMessage("UpdateText");
                    GameBaseController.Instance.Clickable = true;
                }
            }
        }
    }

    private void Update()
    {
        // 当前是否可存档
        if (GameBaseController.Instance.Clickable)
        {
            SettingsButton.SetActive(true);
        }
        else
        {
            SettingsButton.SetActive(false);
        }
        // 控制器
        if (GameBaseController.Instance.GameID == 0)
        {
            if(GameBaseController.Instance.ChapterID == 0)
            {
                if(GameBaseController.Instance.AreaID == 0)
                {
                    if(GameBaseController.Instance.dialogIndex == 0)
                    {
                        GameBaseController.Instance.dialogIndex = 1;
                        Begin0();
                    }
                }
                else if (GameBaseController.Instance.AreaID == 1 && GameBaseController.Instance.dialogIndex == 8)
                {
                    GameBaseController.Instance.SendMessage("ReadText", DialogFile1);
                    showBox();
                    SpeechText.SetActive(false);
                    NewAreaText.SetActive(true);
                    GameBaseController.Instance.dialogIndex = 1;
                    GameBaseController.Instance.ShowDialogRow();
                    GameBaseController.Instance.Clickable = true;
                    OfficeBG.GetComponent<Graphic>().CrossFadeAlpha(0f, 0f, false);
                    IndexPlus();
                }
            }
        }

        //// Area 0 开幕
        //if (AreaID == 0 && GameBaseController.Instance.dialogIndex == 0 && GameBaseController.Instance.dialogIndexNext == 0)
        //{
        //    GameBaseController.Instance.dialogIndexNext = 1;
        //    Begin0();
        //}

        //// Area 1 事务所
        //if (AreaID == 0 && GameBaseController.Instance.dialogIndex == 8 && GameBaseController.Instance.canIClick == 0)
        //{
        //    Begin1();
        //}

        //// Area 1 成步堂来电
        //if (AreaID == 0 && GameBaseController.Instance.dialogIndex == 9 && GameBaseController.Instance.canIClick == 2)
        //{
        //    GameBaseController.Instance.canIClick = 3;
        //    NewAreaText.SetActive(false);
        //    dialogBox.SetActive(false);
        //    SpeechText.SetActive(true);
        //    RingtonePW.Play();
        //    OfficeBG.SetActive(true);
        //    OfficeBG.GetComponent<Graphic>().CrossFadeAlpha(1f, 3f, false);
        //    Invoke("PickUp", 8f);
        //    Invoke("ItemPop", 9f);
        //    Invoke("WrightAnswer", 10f);
        //}

        //if (AreaID == 0 && GameBaseController.Instance.dialogIndex >= 9 && GameBaseController.Instance.dialogIndex < 12)
        //{
        //    OfficeBG.SetActive(true);
        //    ItemHolder.SetActive(true);
        //    dialogBox.SetActive(true);
        //    NameTag.SetActive(true);
        //}

        //if (AreaID == 0 && GameBaseController.Instance.dialogIndex == 12)
        //{
        //    OfficeBG.SetActive(true);
        //    dialogBox.SetActive(true);
        //    NameTag.SetActive(true);
        //}

        //// Area 1 成步堂挂断
        //if (AreaID == 0 && GameBaseController.Instance.dialogIndex == 11)
        //{
        //    GameBaseController.Instance.canIClick = 2;
        //}
        //if (AreaID == 0 && GameBaseController.Instance.dialogIndex == 12 && GameBaseController.Instance.canIClick == 3)
        //{
        //    GameBaseController.Instance.canIClick = 0;
        //    ItemHolder.SetActive(false);
        //    GameBaseController.Instance.ShowDialogRow();
        //}
    }

    private void Begin0()
    {
        hideNameTag();
        GameBaseController.Instance.Clickable = false;
        Invoke("playPrologueBGM", 1f);
        Invoke("showBox", 7f);
        Invoke("dialog", 7f);
        Invoke("dialog", 12f);
        Invoke("dialog", 17f);
        Invoke("dialog", 22f);
        Invoke("dialog", 27f);
        Invoke("dialog", 31f);
        Invoke("dialog", 34f);
        Invoke("hideBox", 37f);
        Invoke("End0", 45f);
    }

    private void playPrologueBGM()
    {
        IntroMusic.Play();
    }

    private void dialog()
    {
        GameBaseController.Instance.ShowDialogRow();
        GameBaseController.Instance.dialogIndex = GameBaseController.Instance.dialogIndex + 1;
    }

    private void showBox()
    {
        dialogBox.SetActive(true);
    }
    private void hideBox()
    {
        dialogBox.SetActive(false);
    }
    private void showNameTag()
    {
        NameTag.SetActive(true);
    }
    private void hideNameTag()
    {
        NameTag.SetActive(false);
    }
    private void End0()
    {
        GameBaseController.Instance.AreaID = 1;
    }
    private void IndexPlus()
    {
        GameBaseController.Instance.dialogIndex = 2;
    }
    private void PickUp()
    {
        RingtonePW.Stop();
        PickUpSFX.Play();
    }

    private void ItemPop()
    {
        ItemHolder.SetActive(true);
        ItemPopSFX.Play();
    }
    private void WrightAnswer()
    {
        NameTag.SetActive(true);
        dialogBox.SetActive(true);
        //GameBaseController.Instance.dialogIndex = GameBaseController.Instance.dialogIndexNext;
        GameBaseController.Instance.ShowDialogRow();
        GameBaseController.Instance.Clickable = true;
    }
}
