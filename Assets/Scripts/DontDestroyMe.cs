using UnityEngine;

public class DontDestroyMe : MonoBehaviour
{
    private static DontDestroyMe instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
