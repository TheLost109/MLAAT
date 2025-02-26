using TMPro;
using UnityEngine;

public class CourtRecords : MonoBehaviour
{
    // 证物名称
    //public TMP_Text EvidenceText;
    // 证物描述
    //public TMP_Text DescText;
    // 证物图片
    //public GameObject EvidencePic;
    // 证物小图
    //public GameObject EvidenceIcon1;
    //public GameObject EvidenceIcon2;
    //public GameObject EvidenceIcon3;
    //public GameObject EvidenceIcon4;
    //public GameObject EvidenceIcon5;
    //public GameObject EvidenceIcon6;
    //public GameObject EvidenceIcon7;
    //public GameObject EvidenceIcon8;
    // 证物页数
    //private int EvidencePage;
    private string[] EvidenceList;
    public int[] InvRecords;
    public int[] InvProfile;

    // 单例模式
    private static CourtRecords _instance;
    public static CourtRecords Instance
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
    private void Update()
    {

    }
    public void ReadRecords(TextAsset _textAsset)
    {
        EvidenceList = _textAsset.text.Split('\n');
        Debug.Log("证物列表读取成功");
    }
    public void NewItem(int ItemID)
    {
        InvRecords[InvRecords.Length - 1] = ItemID;
    }
    public void NewProfile(int PersonID)
    {
        InvProfile[InvProfile.Length - 1] = PersonID;
    }
}
