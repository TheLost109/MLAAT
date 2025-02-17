using System.Collections;
using TMPro;
using UnityEngine;

public class GameBaseController : MonoBehaviour
{
    // 角色姓名Object
    public TMP_Text nameText;
    // 对话内容Object
    public TMP_Text dialogText;
    // 区域介绍Object
    public TMP_Text NewAreaText;
    // 台词位置
    public int dialogIndex;
    // 台词行
    private string[] dialogRows;
    // 游戏ID，0为逆转风暴，1为小马调查员，2为正义之元
    public int GameID;
    // 章节ID
    public int ChapterID;
    // 区域ID
    public int AreaID;
    // 点击音效
    public AudioSource clickSFX;
    // 男说话音效
    public AudioSource speechSFXMale;
    // 女说话音效
    public AudioSource speechSFXFemale;
    // 区域介绍打字音效
    public AudioSource NewAreaSFX;
    // 本句话是否静音
    private bool isSilent = false;
    // 打字机效果延迟
    private float delay;
    // 当前文本
    string currentText;
    // 当前是否在滚动对话
    private bool IsCharacterTalking = false;
    // 当前是否可点击进行下一句
    public bool Clickable = false;
    // 最后一个字符
    private char LastChar;
    // 血量
    public int Health = 10;
    // 
    public string CurrentMusic = "none";
    // 
    public string CurrentBG = "none";

    // 单例模式
    private static GameBaseController _instance;
    public static GameBaseController Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {

    }
    public void UpdateText()
    {
        string[] cells = dialogRows[dialogIndex].Split(',');
        nameText.text = cells[2];
        dialogText.text = cells[3];
    }
    public void ReadText(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split('\n');
        Debug.Log("对话读取成功");
    }
    public void ShowDialogRow()
    {
            IsCharacterTalking = true;
            string[] cells = dialogRows[dialogIndex].Split(',');
            print(cells[0]);
            nameText.text = cells[2];
            if (int.Parse(cells[5]) == 1)
            {
                isSilent = true;
            }
            else if (int.Parse(cells[5]) == 0)
            {
                isSilent = false;
            }
            delay = float.Parse(cells[4]);
            if (int.Parse(cells[0]) == 0)
            {
                StartCoroutine(ShowArea(cells[3], float.Parse(cells[4])));
            }
            else
            {
                StartCoroutine(ShowText(cells[3], int.Parse(cells[1]), float.Parse(cells[4])));
            }
            Invoke("FinishedTalking", delay * cells[3].Length);
    }
    // 对话执行完毕后，将IsCharacterTalking标记为false
    public void FinishedTalking()
    {
        IsCharacterTalking = false;
    }
    // 遍历文本产生打字机效果
    IEnumerator ShowText(string _text, int gender, float delay)
    {
        for (int i = 0; i <= _text.Length; i++)
        {
            currentText = _text.Substring(0, i);
            // 跳过out of bounds字符
            if (i > 0)
            {
                LastChar = _text[i - 1];
                // 狸语SFX
                if (LastChar != '\\')
                {
                    if (!isSilent)
                    {
                        if (gender == 0)
                        {
                            speechSFXFemale.Play();
                        }
                        else
                        {
                            speechSFXMale.Play();
                        }
                    }
                }
                // 跳过换行符
                else if (LastChar == '\\' || LastChar == 'n')
                {
                    yield return new WaitForSeconds(delay);
                }
                // 输出文本
                dialogText.text = currentText;
                // 标点符号停顿
                if (LastChar == '，')
                {
                    yield return new WaitForSeconds(delay + 0.4f);
                }
                else if (LastChar == '…' || LastChar == '。')
                {
                    yield return new WaitForSeconds(delay + 0.9f);
                }
                else if (LastChar == '、')
                {
                    yield return new WaitForSeconds(delay + 0.15f);
                }
                else
                {
                    yield return new WaitForSeconds(delay);
                }
            }
            else
            {
                if (!isSilent)
                {
                    if (gender == 0)
                    {
                        speechSFXFemale.Play();
                    }
                    else
                    {
                        speechSFXMale.Play();
                    }
                }
                dialogText.text = currentText;
                yield return new WaitForSeconds(delay);
            }
        }
    }
    // 展示新区域
    IEnumerator ShowArea(string _text, float delay)
    {
        for (int i = 0; i <= _text.Length; i++)
        {
            currentText = _text.Substring(0, i);
            if (i > 0)
            {
                LastChar = _text[i - 1];
                if (LastChar == '\\' || LastChar == 'n')
                {
                    yield return new WaitForSeconds(delay);
                }
                else
                {
                    NewAreaText.text = currentText;
                    NewAreaSFX.Play();
                    yield return new WaitForSeconds(delay);
                }
            }
            else
            {
                NewAreaText.text = currentText;
                NewAreaSFX.Play();
                yield return new WaitForSeconds(delay);
            }
        }
    }
    public void ClicktoNext()
    {
        if (Clickable)
        {
            if (IsCharacterTalking)
            {
                StopAllCoroutines();
                UpdateText();
                FinishedTalking();
            }
            else
            {
                dialogIndex = dialogIndex + 1;
                clickSFX.Play();
            }
        }
    }
}
