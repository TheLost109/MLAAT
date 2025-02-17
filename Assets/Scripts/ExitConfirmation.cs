using UnityEngine;
using UnityEngine.UI;

public class ExitConfirmation : MonoBehaviour
{
    public GameObject ExitScreen; // �Ի������
    public GameObject ExitPanel; // �Ի������
    public Button confirmButton;   // ȷ�ϰ�ť
    public Button cancelButton;    // ȡ����ť

    private void Start()
    {
        // ���ضԻ���
        ExitScreen.GetComponent<Graphic>().CrossFadeAlpha(0f, 0f, false);

        // �󶨰�ť�¼�
        confirmButton.onClick.AddListener(OnConfirmExit);
        cancelButton.onClick.AddListener(OnCancelExit);
    }

    // ��ʾ�Ի���
    public void ShowDialog()
    {
       ExitScreen.SetActive(true);
       ExitScreen.GetComponent<Graphic>().CrossFadeAlpha(.99f, .25f, false);
       Invoke("ShowPanel", .35f);
    }

    // ȷ���˳�
    private void OnConfirmExit()
    {
        //Debug.Log("�˳���Ϸ��");
        Application.Quit();
    }

    // ȡ���˳�
    private void OnCancelExit()
    {
        ExitScreen.SetActive(false);
        ExitScreen.GetComponent<Graphic>().CrossFadeAlpha(0f, 0f, false);
        ExitPanel.SetActive(false);
    }

    private void ShowPanel()
    {
        ExitPanel.SetActive(true);
    }
}
