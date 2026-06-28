using UnityEngine;
using TMPro; // Required for TextMeshPro
using UnityEngine.UI; // Required for Images

public class AnatomyDisplayManager : MonoBehaviour
{
    [Header("UI Slots")]
    public TextMeshProUGUI titleText;
    //public TextMeshProUGUI locationText;
    public TextMeshProUGUI descriptionText;
    public Image anatomyImageRef;

    // This function receives the data card and updates the UI
    public void DisplayVarmaData(VarmaPointData data)
    {
        if (data == null) return;

        titleText.text = data.pointName;
        //locationText.text = data.location;
        descriptionText.text = data.description;

        // Turn the image on if we have one, off if we don't
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