using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameDisplayComponent : MonoBehaviour
{
    // Set in inspector

    public SpriteRenderer m_SpriteRenderer;

    ////////////////////////////////////////////////////////////////
    
    public Vector2 GetBounds()
    {
        return new Vector2(m_SpriteRenderer.transform.position.x, m_SpriteRenderer.transform.position.y) + m_SpriteRenderer.size;
    }

    /*
    * Use either world space UI or render textures for the minigames. I prefer render textures!
    * Eeach game has its own plyace in the world and the render textures just fit automatically.
    */

}
