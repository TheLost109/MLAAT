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

    // 单例模式
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
        // 检查文件夹是否存在
        if (!Directory.Exists(SavePath))
        {
            // 创建文件夹
            Directory.CreateDirectory(SavePath);
            Debug.Log("新文件夹创建成功: " + SavePath);
        }
        else
        {
            Debug.Log("文件夹已存在");
        }
        SaveData saveData = CreateSaveData();
        //获取当前的游戏数据存在Save对象里
        BinaryFormatter bf = new BinaryFormatter();
        //创建一个二进制形式
        FileStream fs = File.Create(SavePath + "/" + FileName);
        //这里指使用持久路径创建一个文件流并将其保存在systemdata里（具体在哪就不打了，反正创建了）
        //由于持久路径在Windows系统是隐藏的，所以无法找到systemdata本身
        //如果想看到，可以改成dataPath(就像下文json的代码里一样)
        //文件后缀可以随便改，甚至是自定义的（比如我这里用了systemdata）
        bf.Serialize(fs, saveData);
        //将Save对象转化为字节
        fs.Close();
        //把文件流关了
    }
    public void LoadBySerialization(string FileName)
    {
        string SavePath = Application.persistentDataPath + "/Save";
        if (File.Exists(SavePath + "/" + FileName))
        //判断文件是否创建
        {
            //反系列化的过程
            //创建一个二进制格式化程序
            BinaryFormatter bf = new BinaryFormatter();
            //打开一个文件流
            FileStream fs = File.Open(SavePath + "/" + FileName, FileMode.Open);
            //调用格式化程序的反序列化方法，将文件流转换为一个SystemData对象
            SaveData saveData = (SaveData)bf.Deserialize(fs);
            DataCarrier.Instance.GameID = saveData.GameID;
            DataCarrier.Instance.ChapterID = saveData.ChapterID;
            DataCarrier.Instance.AreaID = saveData.AreaID;
            DataCarrier.Instance.dialogIndex = saveData.dialogIndex;
            DataCarrier.Instance.Inventory = saveData.Inventory;
            DataCarrier.Instance.Health = saveData.Health;
            //关闭文件流
            fs.Close();
            DataCarrier.Instance.InGame = true;
            LoadSuccess.SetActive(true);
            LoadSuccess.GetComponent<Graphic>().CrossFadeAlpha(1f, .25f, false);
            Invoke("GoToMainGame", 3f);
        }
        else
        {
            Debug.LogError("读档出现错误。");
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
        //判断文件是否创建
        if (File.Exists(SavePath + "/" + "Save_TS_0"))
        {
            kettei.Play();
            LoadCan.SetActive(true);
            LoadCant.SetActive(false);
            QuestionText.text = "确定要读取以下存档吗？";
            //反系列化的过程
            //创建一个二进制格式化程序
            BinaryFormatter bf = new BinaryFormatter();
            //打开一个文件流
            FileStream fs = File.Open(SavePath + "/Save_TS_0", FileMode.Open);
            //调用格式化程序的反序列化方法，将文件流转换为一个SystemData对象
            SaveData saveData = (SaveData)bf.Deserialize(fs);
            if (saveData.ChapterID == 0) {
                ProgText.text = "第1天：调查";
            }
            else
            {
                ProgText.text = "进度有误";
            }
            DateText.text = saveData.dateTime.ToString("yyyy年MM月dd日 HH:mm");
            //关闭文件流
            fs.Close();
        }
        else
        {
            NoSaveSFX.Play();
            QuestionText.text = "未检测到存档。";
            LoadCan.SetActive(false);
            LoadCant.SetActive(true);
            Debug.Log("未检测到存档");
        }
    }

    public void GoToMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}
