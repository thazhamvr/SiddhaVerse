using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Needed for arrays

public class VarmaPointSender : MonoBehaviour
{
    [Header("Data Card")]
    public VarmaPointData myDataCard;

    // We changed this to an Array to hold multiple spheres!
    private Renderer[] allRenderers;
    private Coroutine blinkRoutine;
    private Color originalColor;
    private Vector3 originalScale;

    void Awake()
    {
        // Find EVERY sphere inside this parent object
        allRenderers = GetComponentsInChildren<Renderer>(true);

        // Grab the original color from the first sphere we find
        if (allRenderers.Length > 0 && allRenderers[0] != null)
        {
            originalColor = allRenderers[0].material.color;
        }

        originalScale = transform.localScale;
    }

    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
        if (!visible) StopBlinking();
    }

    public void StartBlinking()
    {
        StopBlinking();
        if (gameObject.activeInHierarchy)
        {
            blinkRoutine = StartCoroutine(BlinkSequence());
        }
    }

    public void StopBlinking()
    {
        if (blinkRoutine != null) StopCoroutine(blinkRoutine);

        SetAlpha(1f);
        transform.localScale = originalScale;
    }

    private IEnumerator BlinkSequence()
    {
        Vector3 smallScale = originalScale * 0.6f;

        while (true)
        {
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 3.5f;
                SetAlpha(Mathf.Lerp(1f, 0.1f, t));
                transform.localScale = Vector3.Lerp(originalScale, smallScale, t);
                yield return null;
            }

            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 3.5f;
                SetAlpha(Mathf.Lerp(0.1f, 1f, t));
                transform.localScale = Vector3.Lerp(smallScale, originalScale, t);
                yield return null;
            }
        }
    }

    private void SetAlpha(float alpha)
    {
        if (allRenderers == null) return;
        Color c = originalColor;
        c.a = alpha;

        // Loop through EVERY sphere in the group and change its color!
        foreach (Renderer r in allRenderers)
        {
            if (r == null) continue;

            r.material.color = c;
            if (r.material.HasProperty("_BaseColor"))
            {
                r.material.SetColor("_BaseColor", c);
            }
        }
    }

    public void SendData()
    {
        AnatomyDisplayManager uiManager = FindObjectOfType<AnatomyDisplayManager>();

        if (uiManager != null && myDataCard != null)
        {
            uiManager.DisplayVarmaData(myDataCard);
        }
    }
}