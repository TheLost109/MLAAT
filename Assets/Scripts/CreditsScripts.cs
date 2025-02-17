using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScripts : MonoBehaviour
{
    public AudioSource ExitSFX;

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            ExitSFX.Play();
            BacktoMenu();
        }
    }
    public void BacktoMenu()
    {

        SceneManager.LoadScene("Menu");
    }
}
