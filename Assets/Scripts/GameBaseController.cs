using System.Collections;
using TMPro;
using UnityEngine;

public class GameBaseController : MonoBehaviour
{
    // ��ɫ����Object
    public TMP_Text nameText;
    // �Ի�����Object
    public TMP_Text dialogText;
    // �������Object
    public TMP_Text NewAreaText;
    // ̨��λ��
    public int dialogIndex;
    // ̨����
    private string[] dialogRows;
    // ��ϷID��0Ϊ��ת�籩��1ΪС�����Ա��2Ϊ����֮Ԫ
    public int GameID;
    // �½�ID
    public int ChapterID;
    // ����ID
    public int AreaID;
    // �����Ч
    public AudioSource clickSFX;
    // ��˵����Ч
    public AudioSource speechSFXMale;
    // Ů˵����Ч
    public AudioSource speechSFXFemale;
    // ������ܴ�����Ч
    public AudioSource NewAreaSFX;
    // ���仰�Ƿ���
    private bool isSilent = false;
    // ���ֻ�Ч���ӳ�
    private float delay;
    // ��ǰ�ı�
    string currentText;
    // ��ǰ�Ƿ��ڹ����Ի�
    private bool IsCharacterTalking = false;
    // ��ǰ�Ƿ�ɵ��������һ��
    public bool Clickable = false;
    // ���һ���ַ�
    private char LastChar;
    // Ѫ��
    public int Health = 10;
    // 
    public string CurrentMusic = "none";
    // 
    public string CurrentBG = "none";

    // ����ģʽ
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
        Debug.Log("�Ի���ȡ�ɹ�");
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
    // �Ի�ִ����Ϻ󣬽�IsCharacterTalking���Ϊfalse
    public void FinishedTalking()
    {
        IsCharacterTalking = false;
    }
    // �����ı��������ֻ�Ч��
    IEnumerator ShowText(string _text, int gender, float delay)
    {
        for (int i = 0; i <= _text.Length; i++)
        {
            currentText = _text.Substring(0, i);
            // ����out of bounds�ַ�
            if (i > 0)
            {
                LastChar = _text[i - 1];
                // ����SFX
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
                // �������з�
                else if (LastChar == '\\' || LastChar == 'n')
                {
                    yield return new WaitForSeconds(delay);
                }
                // ����ı�
                dialogText.text = currentText;
                // ������ͣ��
                if (LastChar == '��')
                {
                    yield return new WaitForSeconds(delay + 0.4f);
                }
                else if (LastChar == '��' || LastChar == '��')
                {
                    yield return new WaitForSeconds(delay + 0.9f);
                }
                else if (LastChar == '��')
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
    // չʾ������
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
