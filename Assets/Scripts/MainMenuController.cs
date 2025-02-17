using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject SettingsPanel;
    public GameObject BlurMenuBG;

    private void Start()
    {
        Invoke("ShowLogo", 1f);
    }

    public void ShowLogo()
    {
        BlurMenuBG.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ChapSelect");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LookForCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
