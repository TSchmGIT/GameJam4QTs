using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////////////////////////

public enum MinigameType
{
    Sequence    = 0,
    Screwdriver  = 1
}

////////////////////////////////////////////////////////////////

public enum MinigameTickResult
{
    ContinueTick,
    EarlySuccess,
    Failed
}

////////////////////////////////////////////////////////////////

public abstract class BaseMinigame
{
    int                         m_MiniGameID;
    MinigameDisplayComponent    m_DisplayComponent;
    int                         m_PlayerID;

    public Action<MinigameTickResult>  FinishCallback;


    protected Rect              m_GamePlayRect;
    protected Camera            m_MinigameCamera;
    RenderTexture               m_RenderTexture;

    public void Setup(int id, MinigameDisplayComponent displayComponent, Rect rect, int playerID, Action<MinigameTickResult> callback = null)
    {
        m_MiniGameID                            = id;
        m_DisplayComponent                      = displayComponent;
        m_PlayerID                              = playerID;
        m_GamePlayRect                          = rect;
        FinishCallback                          = callback;

        GameObject cameraObject                 = new GameObject("Minigame " + m_MiniGameID);
        cameraObject.transform.localRotation    = Quaternion.Euler(90, 0, 0);
        m_MinigameCamera                        = cameraObject.AddComponent<Camera>();
        m_MinigameCamera.transform.position     = new Vector3(rect.center.x, 5.0f, rect.center.y);
        m_MinigameCamera.orthographic           = true;
        m_MinigameCamera.aspect                 = rect.size.x / rect.size.y;
        m_MinigameCamera.orthographicSize       = rect.size.x / 2.0f;

        m_RenderTexture                         = new RenderTexture((int) rect.size.x * 64, (int) rect.size.y * 64, 24, RenderTextureFormat.Default);
        m_MinigameCamera.targetTexture          = m_RenderTexture;
        m_DisplayComponent.SetRenderTexture(m_RenderTexture);
    }

    ////////////////////////////////////////////////////////////////
   
    public int GetMinigameID()
    {
        return m_MiniGameID;
    }

    ////////////////////////////////////////////////////////////////

    public void CleanUp()
    {
        GameObject.Destroy(m_MinigameCamera.gameObject);
    }

    ////////////////////////////////////////////////////////////////

    public abstract void Start();
    public abstract void Finish();

    public abstract MinigameTickResult Tick();

    ////////////////////////////////////////////////////////////////
    
    protected KeyCode GetKeyCode(InputHelper.Keys key)
    {
        return InputHelper.GetKeyCode(m_PlayerID, key);
    }

    ////////////////////////////////////////////////////////////////
    
    protected bool AnyArrowKeyDown()
    {
        return  Input.GetKeyDown(InputHelper.GetKeyCode(m_PlayerID, InputHelper.Keys.Up)) || 
                Input.GetKeyDown(InputHelper.GetKeyCode(m_PlayerID, InputHelper.Keys.Down)) || 
                Input.GetKeyDown(InputHelper.GetKeyCode(m_PlayerID, InputHelper.Keys.Left)) || 
                Input.GetKeyDown(InputHelper.GetKeyCode(m_PlayerID, InputHelper.Keys.Right));
    }

    ////////////////////////////////////////////////////////////////
    

}
