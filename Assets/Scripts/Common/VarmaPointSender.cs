using UnityEngine;
using System.Collections;

public class VarmaPointSender : MonoBehaviour
{
    [Header("Data Card")]
    public VarmaPointData myDataCard;

    private Renderer[] allRenderers;
    private Coroutine blinkRoutine;
    private Color originalColor;
    private Vector3[] originalScales;

    void Awake()
    {
        allRenderers = GetComponentsInChildren<Renderer>(true);
        originalScales = new Vector3[allRenderers.Length];

        for (int i = 0; i < allRenderers.Length; i++)
        {
            if (allRenderers[i] != null)
            {
                originalScales[i] = allRenderers[i].transform.localScale;
            }
        }

        if (allRenderers.Length > 0 && allRenderers[0] != null)
        {
            originalColor = allRenderers[0].material.color;
        }
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

        for (int i = 0; i < allRenderers.Length; i++)
        {
            if (allRenderers[i] != null)
            {
                allRenderers[i].transform.localScale = originalScales[i];
            }
        }
    }

    private IEnumerator BlinkSequence()
    {
        while (true)
        {
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 3.5f;
                SetAlpha(Mathf.Lerp(1f, 0.1f, t));

                for (int i = 0; i < allRenderers.Length; i++)
                {
                    if (allRenderers[i] != null)
                    {
                        Vector3 smallScale = originalScales[i] * 0.6f;
                        allRenderers[i].transform.localScale = Vector3.Lerp(originalScales[i], smallScale, t);
                    }
                }
                yield return null;
            }

            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 3.5f;
                SetAlpha(Mathf.Lerp(0.1f, 1f, t));

                for (int i = 0; i < allRenderers.Length; i++)
                {
                    if (allRenderers[i] != null)
                    {
                        Vector3 smallScale = originalScales[i] * 0.6f;
                        allRenderers[i].transform.localScale = Vector3.Lerp(smallScale, originalScales[i], t);
                    }
                }
                yield return null;
            }
        }
    }

    private void SetAlpha(float alpha)
    {
        if (allRenderers == null) return;
        Color c = originalColor;
        c.a = alpha;

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
        // Searches inactive objects as well so Canvas_Right_Data is always found
        AnatomyDisplayManager uiManager = FindObjectOfType<AnatomyDisplayManager>(true);

        if (uiManager != null && myDataCard != null)
        {
            uiManager.DisplayVarmaData(myDataCard);
        }
        else
        {
            Debug.LogWarning("SendData failed on " + gameObject.name + " | uiManager: " + uiManager + " | myDataCard: " + myDataCard);
        }
    }
}