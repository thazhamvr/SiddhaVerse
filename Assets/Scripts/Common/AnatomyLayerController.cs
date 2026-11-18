using UnityEngine;

public class AnatomyLayerController : MonoBehaviour
{
    [Header("UI & Points")]
    public GameObject rightDataCanvas;
    public GameObject varmaPointsGroup;

    [Header("Anatomy Folders")]
    public GameObject layer01Skin;
    public GameObject layer02Muscles;
    public GameObject layer03Bones;

    // Internal State: 0 = None, 1 = Skin, 2 = Muscle, 3 = Bone
    private int currentLayer = 0;

    void Start()
    {
        // Start of scene: Hide all anatomy layers
        currentLayer = 0;
        UpdateVisuals();
    }

    // Triggered by the Skin Button
    public void SetSkinLayer()
    {
        currentLayer = 1;
        UpdateVisuals();
    }

    // Triggered by the Muscle Button
    public void SetMuscleLayer()
    {
        currentLayer = 2;
        UpdateVisuals();
    }

    // Triggered by the Bone Button
    public void SetBoneLayer()
    {
        currentLayer = 3;
        UpdateVisuals();
    }

    // Optional: Hide all layers
    public void ClearLayers()
    {
        currentLayer = 0;
        UpdateVisuals();
    }

    // Master Switchboard: Displays the selected layer with its default materials
    private void UpdateVisuals()
    {
        if (layer01Skin != null) layer01Skin.SetActive(currentLayer == 1);
        if (layer02Muscles != null) layer02Muscles.SetActive(currentLayer == 2);
        if (layer03Bones != null) layer03Bones.SetActive(currentLayer == 3);

        // Show points and right data canvas whenever a valid layer is active
        bool showPoints = (currentLayer > 0);
        if (varmaPointsGroup != null) varmaPointsGroup.SetActive(showPoints);
        if (rightDataCanvas != null) rightDataCanvas.SetActive(showPoints);
    }
}