using UnityEngine;
using System.Collections.Generic; // Required for the Dictionary memory bank

public class AnatomyLayerController : MonoBehaviour
{
    [Header("UI & Points")]
    public GameObject rightDataCanvas;
    public GameObject varmaPointsGroup;

    [Header("Anatomy Folders")]
    public GameObject layer01Skin;
    public GameObject layer02Muscles;
    public GameObject layer03Bones;

    [Header("Transparent Materials")]
    public Material matSkinTrans;
    public Material matMuscleTrans;
    public Material matBoneTrans;

    // Internal State
    private int currentLayer = 0; // 0 = None, 1 = Skin, 2 = Muscle, 3 = Bone
    private bool isTransparent = false;

    // Memory Bank to remember every original solid material
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    void Start()
    {
        // 1. Memorize every solid material in the dummy before we do anything else
        MemorizeMaterials(layer01Skin);
        MemorizeMaterials(layer02Muscles);
        MemorizeMaterials(layer03Bones);

        // 2. Start of scene: Hide EVERYTHING
        currentLayer = 0;
        isTransparent = false;
        UpdateVisuals();
    }

    // Scans a folder and saves the original materials of every mesh inside it
    private void MemorizeMaterials(GameObject layerFolder)
    {
        if (layerFolder == null) return;

        // Find every Renderer in the folder, even if it's currently hidden
        Renderer[] allRenderers = layerFolder.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer r in allRenderers)
        {
            originalMaterials[r] = r.sharedMaterials;
        }
    }

    // Triggered by the Skin Button
    public void SetSkinLayer()
    {
        currentLayer = 1;
        isTransparent = false; // Reset to solid when changing layers
        UpdateVisuals();
    }

    // Triggered by the Muscle Button
    public void SetMuscleLayer()
    {
        currentLayer = 2;
        isTransparent = false;
        UpdateVisuals();
    }

    // Triggered by the Bone Button
    public void SetBoneLayer()
    {
        currentLayer = 3;
        isTransparent = false;
        UpdateVisuals();
    }

    // Triggered by your Transparent Button
    public void ToggleTransparency()
    {
        if (currentLayer == 0) return; // Do nothing if the room is empty

        isTransparent = !isTransparent;
        UpdateVisuals();
    }

    // The Master Switchboard
    private void UpdateVisuals()
    {
        // 1. Turn off all 3D folders
        layer01Skin.SetActive(false);
        layer02Muscles.SetActive(false);
        layer03Bones.SetActive(false);

        // 2. Points and UI only show if we are in Transparent mode AND a body is selected
        bool showPoints = (currentLayer > 0 && isTransparent);
        varmaPointsGroup.SetActive(showPoints);
        rightDataCanvas.SetActive(showPoints);

        // 3. Turn on the correct body and apply transparency
        if (currentLayer == 1)
        {
            layer01Skin.SetActive(true);
            ApplyMaterialState(layer01Skin, matSkinTrans);
        }
        else if (currentLayer == 2)
        {
            layer02Muscles.SetActive(true);
            ApplyMaterialState(layer02Muscles, matMuscleTrans);
        }
        else if (currentLayer == 3)
        {
            layer03Bones.SetActive(true);
            ApplyMaterialState(layer03Bones, matBoneTrans);
        }
    }

    // Swaps materials for every single mesh inside a folder
    private void ApplyMaterialState(GameObject layerFolder, Material transparentMat)
    {
        Renderer[] allRenderers = layerFolder.GetComponentsInChildren<Renderer>(true);

        foreach (Renderer r in allRenderers)
        {
            if (isTransparent && transparentMat != null)
            {
                // Create an array of transparent materials that matches the length of the original
                // (This perfectly fixes your 3-material muscle problem!)
                Material[] transArray = new Material[originalMaterials[r].Length];
                for (int i = 0; i < transArray.Length; i++)
                {
                    transArray[i] = transparentMat;
                }
                r.materials = transArray;
            }
            else
            {
                // Restore the original solid materials from the memory bank
                r.materials = originalMaterials[r];
            }
        }
    }
}