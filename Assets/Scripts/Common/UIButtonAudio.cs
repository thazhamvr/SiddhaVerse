using UnityEngine;
using UnityEngine.EventSystems;

// 1. Changed IPointerClickHandler to IPointerDownHandler
public class UIButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [Header("Audio Clips")]
    public AudioClip hoverSound;
    public AudioClip clickSound;

    [Header("Audio Source")]
    public AudioSource audioSource;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponentInParent<AudioSource>();
            if (audioSource == null && Camera.main != null)
            {
                audioSource = Camera.main.GetComponent<AudioSource>();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    // 2. Changed OnPointerClick to OnPointerDown
    public void OnPointerDown(PointerEventData eventData)
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}