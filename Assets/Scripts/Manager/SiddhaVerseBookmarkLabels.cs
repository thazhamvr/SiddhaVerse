using UnityEngine;

/// <summary>
/// Keeps the world-space bookmark label in sync with its Toggle state.
/// </summary>
public sealed class SiddhaVerseBookmarkLabels : MonoBehaviour
{
    [SerializeField] private GameObject saveLabel;
    [SerializeField] private GameObject savedLabel;

    public void SetSaved(bool saved)
    {
        if (saveLabel != null) saveLabel.SetActive(!saved);
        if (savedLabel != null) savedLabel.SetActive(saved);
    }
}
