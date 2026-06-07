using UnityEngine;
using TMPro;

[ExecuteAlways]
public class VarmaUIManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject calloutPrefab;

    [Header("Editor Tools (Click these!)")]
    public bool generateUI = false;
    public bool clearUI = false;

    void Update()
    {
        if (generateUI)
        {
            generateUI = false;
            GenerateAllCallouts();
        }

        if (clearUI)
        {
            clearUI = false;
            ClearCallouts();
        }
    }

    void GenerateAllCallouts()
    {
        ClearCallouts();

        if (calloutPrefab == null) return;

        VarmaNode[] allPoints = FindObjectsOfType<VarmaNode>();

        foreach (VarmaNode node in allPoints)
        {
            if (node.pointData == null) continue;

            GameObject spawnedUI = Instantiate(calloutPrefab, this.transform);

            // MATH FIX: Read the mesh center position
            Vector3 sphereCenter = node.transform.position;
            Renderer meshRenderer = node.GetComponentInChildren<Renderer>();
            if (meshRenderer != null)
            {
                sphereCenter = meshRenderer.bounds.center;
            }

            // DYNAMIC POSITION: Use the custom offset saved inside this specific ScriptableObject!
            spawnedUI.transform.position = sphereCenter + node.pointData.uiOffset;

            TextMeshProUGUI uiText = spawnedUI.GetComponentInChildren<TextMeshProUGUI>();
            if (uiText != null)
            {
                uiText.text = node.pointData.pointName;
            }

            UICalloutLine calloutLine = spawnedUI.GetComponent<UICalloutLine>();
            if (calloutLine != null)
            {
                calloutLine.bodyPoint = node.transform;
            }
        }
    }

    void ClearCallouts()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}