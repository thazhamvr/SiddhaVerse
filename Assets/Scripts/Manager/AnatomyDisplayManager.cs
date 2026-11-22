using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnatomyDisplayManager : MonoBehaviour
{
    [Header("UI Slots")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI locationText;

    [Header("Additional UI Slots")]
    public TextMeshProUGUI locationDetailText;
    public TextMeshProUGUI traditionalUseText;
    public TextMeshProUGUI detailedInfoText;

    [Header("Main Tab Display & Image")]
    public TextMeshProUGUI sectionHeaderText; // Dynamic_text (1) -> "DESCRIPTION" or "LOCATION"
    public TextMeshProUGUI descriptionText;   // Dynamic_text -> description or location body text
    public Image anatomyImageRef;

    [Header("ElevenLabs Audio Guide")]
    [Tooltip("Add an AudioSource component to Canvas_Right_Data and drag it here")]
    public AudioSource narrationAudioSource;
    [Tooltip("Optional: Drag the Audio Guide button's Text (TMP) here to toggle label between Play/Stop")]
    public TextMeshProUGUI audioButtonLabel;

    private VarmaPointData currentData;

    void Awake()
    {
        if (narrationAudioSource != null)
        {
            narrationAudioSource.playOnAwake = false;
            narrationAudioSource.clip = null;
        }
    }

    void Update()
    {
        // Automatically reset button text when the ElevenLabs audio clip finishes playing
        if (narrationAudioSource != null && !narrationAudioSource.isPlaying && audioButtonLabel != null)
        {
            if (audioButtonLabel.text == "STOP AUDIO")
            {
                audioButtonLabel.text = "AUDIO GUIDE";
            }
        }
    }

    void OnDisable()
    {
        // Stop and clear audio when walking out of the TV trigger zone or closing the UI
        StopAudio();
    }

    public void DisplayVarmaData(VarmaPointData data)
    {
        if (data == null) return;

        // Stop previous point's audio when loading a newly selected Varmam point
        StopAudio();

        currentData = data;

        if (titleText != null) titleText.text = data.pointName;

        if (locationText != null) locationText.text = data.location;
        if (locationDetailText != null) locationDetailText.text = data.location;
        if (traditionalUseText != null) traditionalUseText.text = data.description;
        if (detailedInfoText != null) detailedInfoText.text = data.description;

        // Defaults to Overview (Description) right after View is pressed
        ShowOverview();

        if (anatomyImageRef != null)
        {
            if (data.anatomyImage != null)
            {
                anatomyImageRef.sprite = data.anatomyImage;
                anatomyImageRef.enabled = true;
            }
            else
            {
                anatomyImageRef.enabled = false;
            }
        }
    }

    // Hook to OverView_btn -> OnClick()
    public void ShowOverview()
    {
        if (currentData == null) return;

        if (sectionHeaderText != null)
            sectionHeaderText.text = "DESCRIPTION";

        if (descriptionText != null)
            descriptionText.text = currentData.description;
    }

    // Hook to Location_btn (1) -> OnClick()
    public void ShowLocation()
    {
        if (currentData == null) return;

        if (sectionHeaderText != null)
            sectionHeaderText.text = "LOCATION";

        if (descriptionText != null)
            descriptionText.text = currentData.location;
    }

    // Hook to the AUDIO GUIDE button at the bottom-left of Canvas_Right_Data -> OnClick()
    public void ToggleDescriptionAudio()
    {
        if (narrationAudioSource == null || currentData == null || currentData.descriptionAudio == null)
        {
            Debug.LogWarning("Missing AudioSource or Description Audio clip for this Varmam point.");
            return;
        }

        if (narrationAudioSource.isPlaying)
        {
            StopAudio();
        }
        else
        {
            narrationAudioSource.clip = currentData.descriptionAudio;
            narrationAudioSource.Play();

            if (audioButtonLabel != null)
                audioButtonLabel.text = "STOP AUDIO";
        }
    }

    public void StopAudio()
    {
        if (narrationAudioSource != null)
        {
            if (narrationAudioSource.isPlaying)
            {
                narrationAudioSource.Stop();
            }
            narrationAudioSource.clip = null;
        }

        if (audioButtonLabel != null)
            audioButtonLabel.text = "AUDIO GUIDE";
    }
}