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
    public TextMeshProUGUI sectionHeaderText; // Dynamic_text (1) -> displays "DESCRIPTION" or "LOCATION"
    public TextMeshProUGUI descriptionText;   // Dynamic_text -> displays description or location body text
    public Image anatomyImageRef;

    private VarmaPointData currentData;

    public void DisplayVarmaData(VarmaPointData data)
    {
        if (data == null) return;

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
}