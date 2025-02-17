using UnityEngine;
using UnityEngine.UI;

public class ExitConfirmation : MonoBehaviour
{
    public GameObject ExitScreen; // 对话框面板
    public GameObject ExitPanel; // 对话框面板
    public Button confirmButton;   // 确认按钮
    public Button cancelButton;    // 取消按钮

    private void Start()
    {
        // 隐藏对话框
        ExitScreen.GetComponent<Graphic>().CrossFadeAlpha(0f, 0f, false);

        // 绑定按钮事件
        confirmButton.onClick.AddListener(OnConfirmExit);
        cancelButton.onClick.AddListener(OnCancelExit);
    }

    // 显示对话框
    public void ShowDialog()
    {
       ExitScreen.SetActive(true);
       ExitScreen.GetComponent<Graphic>().CrossFadeAlpha(.99f, .25f, false);
       Invoke("ShowPanel", .35f);
    }

    // 确认退出
    private void OnConfirmExit()
    {
        //Debug.Log("退出游戏！");
        Application.Quit();
    }

    // 取消退出
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
