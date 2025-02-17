using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SystemDataScripts : MonoBehaviour
{
    // ����ģʽ
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
        SystemData systemData = CreateSysData();
        //��ȡ��ǰ����Ϸ���ݴ���Save������
        BinaryFormatter bf = new BinaryFormatter();
        //����һ����������ʽ
        FileStream fs = File.Create(SavePath + "/systemdata");
        //����ָʹ�ó־�·������һ���ļ��������䱣����systemdata��������ľͲ����ˣ����������ˣ�
        //���ڳ־�·����Windowsϵͳ�����صģ������޷��ҵ�systemdata����
        //����뿴�������Ըĳ�dataPath(��������json�Ĵ�����һ��)
        //�ļ���׺�������ģ��������Զ���ģ���������������systemdata��
        bf.Serialize(fs, systemData);
        //��Save����ת��Ϊ�ֽ�
        fs.Close();
        //���ļ�������
    }
    public void LoadBySerialization()
    {
        string SavePath = Application.persistentDataPath + "/Save";
        if (File.Exists(SavePath + "/systemdata"))
        //�ж��ļ��Ƿ񴴽�
        {
            //��ϵ�л��Ĺ���
            //����һ�������Ƹ�ʽ������
            BinaryFormatter bf = new BinaryFormatter();
            //��һ���ļ���
            FileStream fs = File.Open(SavePath + "/systemdata", FileMode.Open);
            //���ø�ʽ������ķ����л����������ļ���ת��Ϊһ��SystemData����
            SystemData systemData = (SystemData)bf.Deserialize(fs);
            SettingsScripts.Instance.VoiceLang = systemData.VoiceLang;
            SettingsScripts.Instance.BGMSlider.value = systemData.BGMVol;
            SettingsScripts.Instance.SFXSlider.value = systemData.SFXVol;
            //�ر��ļ���
            fs.Close();
        }
        else
        {
            Debug.LogError("δ��⵽systemdata�����ڴ�����");
            SaveBySerialization();
        }
    }

}
