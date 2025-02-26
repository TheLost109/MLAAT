using TMPro;
using UnityEngine;

public class CourtRecords : MonoBehaviour
{
    // ֤������
    //public TMP_Text EvidenceText;
    // ֤������
    //public TMP_Text DescText;
    // ֤��ͼƬ
    //public GameObject EvidencePic;
    // ֤��Сͼ
    //public GameObject EvidenceIcon1;
    //public GameObject EvidenceIcon2;
    //public GameObject EvidenceIcon3;
    //public GameObject EvidenceIcon4;
    //public GameObject EvidenceIcon5;
    //public GameObject EvidenceIcon6;
    //public GameObject EvidenceIcon7;
    //public GameObject EvidenceIcon8;
    // ֤��ҳ��
    //private int EvidencePage;
    private string[] EvidenceList;
    public int[] InvRecords;
    public int[] InvProfile;

    // ����ģʽ
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
        Debug.Log("֤���б��ȡ�ɹ�");
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
