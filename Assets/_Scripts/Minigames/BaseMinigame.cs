using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////////////////////////

public enum MinigameType
{
    Sequence    = 0,
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
    MinigameDisplayComponent    m_DisplayComponent;
    int                         m_PlayerID;

    public void Setup(MinigameDisplayComponent displayComponent, int playerID)
    {
        m_DisplayComponent  = displayComponent;
        m_PlayerID          = playerID;
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
}
