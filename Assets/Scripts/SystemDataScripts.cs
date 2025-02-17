using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SystemDataScripts : MonoBehaviour
{
    // 单例模式
    private static SystemDataScripts _instance;
    public static SystemDataScripts Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        LoadBySerialization();
    }

    [Serializable]
    public class SystemData
    {
        public int VoiceLang;
        public float BGMVol;
        public float SFXVol;
    }

    public SystemData CreateSysData()
    {
        SystemData systemData = new SystemData();
        systemData.VoiceLang = SettingsScripts.Instance.VoiceLang;
        systemData.BGMVol = SettingsScripts.Instance.BGMSlider.value;
        systemData.SFXVol = SettingsScripts.Instance.SFXSlider.value;
        return systemData;
    }

    public void SaveBySerialization()
    {
        string SavePath = Application.persistentDataPath + "/Save";
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
        SystemData systemData = CreateSysData();
        //获取当前的游戏数据存在Save对象里
        BinaryFormatter bf = new BinaryFormatter();
        //创建一个二进制形式
        FileStream fs = File.Create(SavePath + "/systemdata");
        //这里指使用持久路径创建一个文件流并将其保存在systemdata里（具体在哪就不打了，反正创建了）
        //由于持久路径在Windows系统是隐藏的，所以无法找到systemdata本身
        //如果想看到，可以改成dataPath(就像下文json的代码里一样)
        //文件后缀可以随便改，甚至是自定义的（比如我这里用了systemdata）
        bf.Serialize(fs, systemData);
        //将Save对象转化为字节
        fs.Close();
        //把文件流关了
    }
    public void LoadBySerialization()
    {
        string SavePath = Application.persistentDataPath + "/Save";
        if (File.Exists(SavePath + "/systemdata"))
        //判断文件是否创建
        {
            //反系列化的过程
            //创建一个二进制格式化程序
            BinaryFormatter bf = new BinaryFormatter();
            //打开一个文件流
            FileStream fs = File.Open(SavePath + "/systemdata", FileMode.Open);
            //调用格式化程序的反序列化方法，将文件流转换为一个SystemData对象
            SystemData systemData = (SystemData)bf.Deserialize(fs);
            SettingsScripts.Instance.VoiceLang = systemData.VoiceLang;
            SettingsScripts.Instance.BGMSlider.value = systemData.BGMVol;
            SettingsScripts.Instance.SFXSlider.value = systemData.SFXVol;
            //关闭文件流
            fs.Close();
        }
        else
        {
            Debug.LogError("未检测到systemdata，正在创建。");
            SaveBySerialization();
        }
    }

}
