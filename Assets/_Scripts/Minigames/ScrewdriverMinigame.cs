using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewdriverMinigame : BaseMinigame
{
    #region Unity References

    public float m_MinTargetSize    = 0.5f;
    public float m_MaxTargetSize    = 2.0f;
    public float m_MinPinSpeed      = 6.0f;
    public float m_MaxPinSpeed      = 9.0f;

    private float m_TargetSize      = 0.25f;
    private float m_PinSpeed        = 0.2f;
    private bool  m_PinMovesLeft    = false;

    private float m_LeftTargetBound       = -0.5f;
    private float m_RightTargetBound      = 0.5f;

    private float m_LeftTarget;
    private float m_RightTarget;
    
    private float m_BarSpawn      = 3.0f;

    private GameObject m_PinObject = null;
    private GameObject m_TargetObject = null;
    
    #endregion

    public override void Start()
    {
        Debug.Log("Screwdriver Game Started");

        m_TargetSize    = UnityEngine.Random.Range(m_MinTargetSize, m_MaxTargetSize);
        m_PinSpeed      = UnityEngine.Random.Range(m_MinPinSpeed, m_MaxPinSpeed);
        Debug.Log("Target Size = " + m_TargetSize);
        Debug.Log("Pin Speed = " + m_PinSpeed);

        m_TargetObject = GameObject.Instantiate(GameManager.Instance.settings.ScrewDriverTargetPrefab, new Vector3(m_GamePlayRect.center.x, 0, m_GamePlayRect.center.y), Quaternion.identity);
        m_TargetObject.transform.parent = m_MinigameCamera.transform;

        m_PinObject = GameObject.Instantiate(GameManager.Instance.settings.ScrewDriverPinPrefab, new Vector3(m_GamePlayRect.center.x, 0, m_GamePlayRect.center.y) - Vector3.right * m_BarSpawn, Quaternion.identity);
        m_PinObject.transform.parent = m_MinigameCamera.transform;

        m_TargetObject.transform.localScale = new Vector3(m_TargetSize, 1f, 1f);
        m_LeftTarget                        = m_GamePlayRect.center.x + m_TargetSize * m_LeftTargetBound;
        m_RightTarget                       = m_GamePlayRect.center.x + m_TargetSize * m_RightTargetBound;
    }

    public override MinigameTickResult Tick()
    {
        // Pin Movement
        if (m_PinMovesLeft == false)
        {
            m_PinObject.transform.Translate(Vector3.right * Time.deltaTime * m_PinSpeed);
        }

        if (m_PinObject.transform.localPosition.x > m_TargetObject.transform.localPosition.x + m_BarSpawn)
        {
            m_PinMovesLeft = true;
        }

        if (m_PinObject.transform.localPosition.x < m_TargetObject.transform.localPosition.x - m_BarSpawn)
        {
            m_PinMovesLeft = false;
        }

        if (m_PinMovesLeft == true)
        {
            m_PinObject.transform.Translate(Vector3.left * Time.deltaTime * m_PinSpeed);
        }

        if (Input.GetKeyDown(GetKeyCode(InputHelper.Keys.Action)))
        {
            m_PinSpeed = 0;
            return CheckPlayerSuccess();
        }
        else
        {
            return MinigameTickResult.ContinueTick;
        }
    }

    ////////////////////////////////////////////////////////////////

    MinigameTickResult CheckPlayerSuccess()
    {
        if (m_PinObject.transform.position.x > m_LeftTarget && 
            m_PinObject.transform.position.x < m_RightTarget)
        {
            return MinigameTickResult.EarlySuccess;
        }

        else
        {
            return MinigameTickResult.Failed;
        }
    }

    public override void Finish()
    {
        GameObject.Destroy(m_TargetObject);
        GameObject.Destroy(m_PinObject);
    }

}
