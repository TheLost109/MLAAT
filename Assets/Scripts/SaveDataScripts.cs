using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveDataScripts : MonoBehaviour
{
    public TMP_Text DateText;
    public TMP_Text ProgText;
    public TMP_Text QuestionText;
    public GameObject LoadCan;
    public GameObject LoadCant;
    public GameObject LoadSuccess;

    public AudioSource kettei;
    public AudioSource NoSaveSFX;

    // ����ģʽ
    private static SaveDataScripts _instance;
    public static SaveDataScripts Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        if (DataCarrier.Instance.InGame == true)
        {
            LoadByCarrier();
        }
    }

    [Serializable]
    public class SaveData
    {
        public int FileID;
        public DateTime dateTime;
        public int GameID;
        public int ChapterID;
        public int AreaID;
        public int dialogIndex;
        public int[] Inventory;
        public int Health;
    }

    public SaveData CreateSaveData()
    {
        SaveData saveData = new SaveData();
        saveData.FileID = DataCarrier.Instance.FileID;
        saveData.dateTime = DateTime.Now;
        saveData.GameID = GameBaseController.Instance.GameID;
        saveData.ChapterID = GameBaseController.Instance.ChapterID;
        saveData.AreaID = GameBaseController.Instance.AreaID;
        saveData.dialogIndex = GameBaseController.Instance.dialogIndex;
        saveData.Inventory = CourtRecords.Instance.Inventory;
        saveData.Health = GameBaseController.Instance.Health;
        return saveData;
    }

    public void SaveBySerialization()
    {
        string SavePath = Application.persistentDataPath + "/Save";
        string FileName = "UnknownData";
        if(GameBaseController.Instance.GameID == 0)
        {
            FileName = "Save_TS_0";
        }
        // ����ļ����Ƿ����
        if (!Directory.Exists(SavePath))
        {
            // �����ļ���
            Directory.CreateDirectory(SavePath);
            Debug.Log("���ļ��д����ɹ�: " + SavePath);
        }
        else
        {
            Debug.Log("�ļ����Ѵ���");
        }
        SaveData saveData = CreateSaveData();
        //��ȡ��ǰ����Ϸ���ݴ���Save������
        BinaryFormatter bf = new BinaryFormatter();
        //����һ����������ʽ
        FileStream fs = File.Create(SavePath + "/" + FileName);
        //����ָʹ�ó־�·������һ���ļ��������䱣����systemdata��������ľͲ����ˣ����������ˣ�
        //���ڳ־�·����Windowsϵͳ�����صģ������޷��ҵ�systemdata����
        //����뿴�������Ըĳ�dataPath(��������json�Ĵ�����һ��)
        //�ļ���׺�������ģ��������Զ���ģ���������������systemdata��
        bf.Serialize(fs, saveData);
        //��Save����ת��Ϊ�ֽ�
        fs.Close();
        //���ļ�������
    }
    public void LoadBySerialization(string FileName)
    {
        string SavePath = Application.persistentDataPath + "/Save";
        if (File.Exists(SavePath + "/" + FileName))
        //�ж��ļ��Ƿ񴴽�
        {
            //��ϵ�л��Ĺ���
            //����һ�������Ƹ�ʽ������
            BinaryFormatter bf = new BinaryFormatter();
            //��һ���ļ���
            FileStream fs = File.Open(SavePath + "/" + FileName, FileMode.Open);
            //���ø�ʽ������ķ����л����������ļ���ת��Ϊһ��SystemData����
            SaveData saveData = (SaveData)bf.Deserialize(fs);
            DataCarrier.Instance.GameID = saveData.GameID;
            DataCarrier.Instance.ChapterID = saveData.ChapterID;
            DataCarrier.Instance.AreaID = saveData.AreaID;
            DataCarrier.Instance.dialogIndex = saveData.dialogIndex;
            DataCarrier.Instance.Inventory = saveData.Inventory;
            DataCarrier.Instance.Health = saveData.Health;
            //�ر��ļ���
            fs.Close();
            DataCarrier.Instance.InGame = true;
            LoadSuccess.SetActive(true);
            LoadSuccess.GetComponent<Graphic>().CrossFadeAlpha(1f, .25f, false);
            Invoke("GoToMainGame", 3f);
        }
        else
        {
            Debug.LogError("�������ִ���");
        }
    }

    public void LoadByCarrier()
    {
        GameBaseController.Instance.GameID = DataCarrier.Instance.GameID;
        GameBaseController.Instance.ChapterID = DataCarrier.Instance.ChapterID;
        GameBaseController.Instance.AreaID = DataCarrier.Instance.AreaID;
        GameBaseController.Instance.dialogIndex = DataCarrier.Instance.dialogIndex;
    }
    public void LoadSaveInfo()
    {
        string SavePath = Application.persistentDataPath + "/Save";
        //�ж��ļ��Ƿ񴴽�
        if (File.Exists(SavePath + "/" + "Save_TS_0"))
        {
            kettei.Play();
            LoadCan.SetActive(true);
            LoadCant.SetActive(false);
            QuestionText.text = "ȷ��Ҫ��ȡ���´浵��";
            //��ϵ�л��Ĺ���
            //����һ�������Ƹ�ʽ������
            BinaryFormatter bf = new BinaryFormatter();
            //��һ���ļ���
            FileStream fs = File.Open(SavePath + "/Save_TS_0", FileMode.Open);
            //���ø�ʽ������ķ����л����������ļ���ת��Ϊһ��SystemData����
            SaveData saveData = (SaveData)bf.Deserialize(fs);
            if (saveData.ChapterID == 0) {
                ProgText.text = "��1�죺����";
            }
            else
            {
                ProgText.text = "��������";
            }
            DateText.text = saveData.dateTime.ToString("yyyy��MM��dd�� HH:mm");
            //�ر��ļ���
            fs.Close();
        }
        else
        {
            NoSaveSFX.Play();
            QuestionText.text = "δ��⵽�浵��";
            LoadCan.SetActive(false);
            LoadCant.SetActive(true);
            Debug.Log("δ��⵽�浵");
        }
    }

    public void GoToMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}
