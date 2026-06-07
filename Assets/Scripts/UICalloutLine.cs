using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
public class UICalloutLine : MonoBehaviour
{
    [Header("Callout Targets")]
    public Transform bodyPoint;
    public Transform uiLabel;

    private LineRenderer lineRenderer;
    private Renderer meshRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void LateUpdate()
    {
        if (bodyPoint != null && uiLabel != null)
        {
            // 1. Try to find the visual 3D mesh attached to your target point
            if (meshRenderer == null)
            {
                meshRenderer = bodyPoint.GetComponentInChildren<Renderer>();
            }

            Vector3 targetPosition;

            // 2. If it finds the mesh, snap to its visual center (ignoring bad pivots)
            if (meshRenderer != null)
            {
                targetPosition = meshRenderer.bounds.center;
            }
            // 3. Fallback just in case there is no mesh
            else
            {
                targetPosition = bodyPoint.position;
            }

            // Draw the line!
            lineRenderer.SetPosition(0, targetPosition);
            lineRenderer.SetPosition(1, uiLabel.position);
        }
    }
}