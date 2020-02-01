using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameDisplayComponent : MonoBehaviour
{
    // Set in inspector

    public MeshRenderer m_MeshRenderer;

    ////////////////////////////////////////////////////////////////
    
    public void SetRenderTexture(RenderTexture texture)
    {
        m_MeshRenderer.material.SetTexture("_MainTex", texture);
    }
}
