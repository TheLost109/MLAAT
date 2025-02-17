using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameFunc : MonoBehaviour
{
    public GameObject SaveScreen;
    public GameObject SavePanel;
    public GameObject SaveComplete;

    private void Awake()
    {
        SaveScreen.GetComponent<Graphic>().CrossFadeAlpha(0f, 0f, false);
    }

    // ��ʾ
    public void ShowDialog()
    {
        SaveScreen.SetActive(true);
        SaveScreen.GetComponent<Graphic>().CrossFadeAlpha(.99f, .25f, false);
        Invoke("ShowPanel", .35f);
    }

    private void ShowPanel()
    {
        SavePanel.SetActive(true);
    }


    // ȷ�ϴ浵
    public void OnConfirmSave()
    {
        SaveDataScripts.Instance.SaveBySerialization();
        SaveComplete.SetActive(true);
        Invoke("ReturnToChapSel", 3f);
    }

    // ȡ���˳�
    public void OnCancelSave()
    {
        SaveScreen.SetActive(false);
        SaveScreen.GetComponent<Graphic>().CrossFadeAlpha(0f, 0f, false);
        SavePanel.SetActive(false);
    }

    private void ReturnToChapSel()
    {
        SceneManager.LoadScene("ChapSelect");
    }
}