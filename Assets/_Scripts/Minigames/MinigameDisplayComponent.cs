using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameDisplayComponent : MonoBehaviour
{
    // Set in inspector

    public MeshRenderer m_MeshRenderer;
    Color baseColor = Color.black;
    public VolumetricLines.VolumetricLineBehavior[] m_Lines = null;

    private void Start()
    {
        foreach (VolumetricLines.VolumetricLineBehavior line in m_Lines)
        {
            line.LineColor = baseColor;
        }
    }

    ////////////////////////////////////////////////////////////////

    public void SetRenderTexture(RenderTexture texture)
    {
        m_MeshRenderer.material.SetTexture("_MainTex", texture);
    }

    public void ReactToMinigameResult(MinigameTickResult result)
    {
        StartCoroutine(C_AnimatedLightFlash(result));
    }

    IEnumerator C_AnimatedLightFlash(MinigameTickResult result)
    {
        float time = 0.0f;
        float totalTime = 0.5f;

        Color startColor = baseColor;
        Color targetColor = result == MinigameTickResult.EarlySuccess ? Color.green : Color.red;

        Debug.Log("Animate Success/Fail");

        while (time < totalTime)
        {
            time += Time.deltaTime;
            foreach (VolumetricLines.VolumetricLineBehavior line in m_Lines)
            {
                line.LineColor = Color.Lerp(startColor, targetColor, time / totalTime);
            }

            yield return new WaitForEndOfFrame();
        }

        time = 0.0f;
        while (time < totalTime)
        {
            time += Time.deltaTime;
            foreach (VolumetricLines.VolumetricLineBehavior line in m_Lines)
            {
                line.LineColor = Color.Lerp(startColor, targetColor, 1.0f - time / totalTime);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
