using UnityEngine;
using TMPro;

/// <summary>
/// ´úÂëÀ´×Ô£ºhttps://discussions.unity.com/t/textmesh-pro-change-bold-underline-at-runtime/690647
/// </summary>
public class UnderlineWhenHover : MonoBehaviour
{
    public TMP_Text Text;

    private void Start()
    {

    }

    public void AddUnderline()
    {
        // Assign the underline style to the text component.
        Text.fontStyle = FontStyles.Underline;
    }

    public void RemoveUnderline()
    {
        // Assign the underline style to the text component.
        Text.fontStyle = FontStyles.Normal;
    }
}
