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

    public TextMeshProUGUI descriptionText;
    public Image anatomyImageRef;

    public void DisplayVarmaData(VarmaPointData data)
    {
        if (data == null) return;

        titleText.text = data.pointName;

        // Push the location text ONLY to the right canvas
        if (locationText != null) locationText.text = data.location;
        if (locationDetailText != null) locationDetailText.text = data.location;

        descriptionText.text = data.description;
        if (traditionalUseText != null) traditionalUseText.text = data.description;
        if (detailedInfoText != null) detailedInfoText.text = data.description;

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
