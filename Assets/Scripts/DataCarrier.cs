using UnityEngine;

public class DataCarrier : MonoBehaviour
{
    public int FileID = 0;
    public int GameID;
    public int ChapterID;
    public int AreaID;
    public int dialogIndex;
    public int Health;
    public int[] Inventory;
    public bool InGame = false;

    // µ¥ÀýÄ£Ê½
    private static DataCarrier _instance;
    public static DataCarrier Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}