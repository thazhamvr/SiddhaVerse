using UnityEngine;

public class AnatomyLayerController : MonoBehaviour
{
    [Header("Anatomy Folders")]
    public GameObject layer01Skin;
    public GameObject layer02Muscles;
    public GameObject layer03Bones;
    public GameObject varmaPointsGroup;

    [Header("Skin Materials")]
    public Renderer skinMeshRenderer; // The 3D component that draws the skin
    public Material matSolidSkin;     // Your normal skin material
    public Material matTransparentSkin; // The Mat_SkinHologram we made earlier

    void Start()
    {
        // Set the default state to Skin View when the game starts
        SetSkinView();
    }

    // Triggered by the Skin Button
    public void SetSkinView()
    {
        layer01Skin.SetActive(true);
        layer02Muscles.SetActive(false);
        layer03Bones.SetActive(false);
        varmaPointsGroup.SetActive(false); // Hide Varma points

        if (skinMeshRenderer != null && matSolidSkin != null)
        {
            skinMeshRenderer.material = matSolidSkin;
        }
    }

    // Triggered by the Muscles/Nerves Button
    public void SetMuscleView()
    {
        layer01Skin.SetActive(true);
        layer02Muscles.SetActive(true);
        layer03Bones.SetActive(false);
        varmaPointsGroup.SetActive(true); // Show glowing Varma points

        if (skinMeshRenderer != null && matTransparentSkin != null)
        {
            skinMeshRenderer.material = matTransparentSkin; // Turn skin to glass
        }
    }

    // Triggered by the Bones Button
    public void SetBoneView()
    {
        layer01Skin.SetActive(false); // Hide skin completely 
        layer02Muscles.SetActive(false);
        layer03Bones.SetActive(true); // Show bones
        varmaPointsGroup.SetActive(true); // Show glowing Varma points
    }
}