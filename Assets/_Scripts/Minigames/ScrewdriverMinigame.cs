using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewdriverMinigame : BaseMinigame
{
    #region Unity References

    public float m_MinTargetSize = 0.1f;
    public float m_MaxTargetSize = 0.5f;
    public float m_MinLineSpeed = 0.1f;
    public float m_MaxLineSpeed = 0.5f;

    #endregion

    public override void Start()
    {
        Debug.Log("Game Started");
    }

    public override MinigameTickResult Tick()
    {
        return MinigameTickResult.ContinueTick;
    }

    public override void Finish()
    {
    }

}
