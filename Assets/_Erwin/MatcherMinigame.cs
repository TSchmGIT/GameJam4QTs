using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatcherMinigame : BaseMinigame
{
    int m_IndexMiddle = 0;
    int m_IndexUp = 0;
    int m_IndexRight = 0;
    int m_IndexDown = 0;
    int m_IndexLeft = 0;
    
    Texture2D pickedVirusTarget    = null;
    Texture2D pickedVirusUp        = null;
    Texture2D pickedVirusRight     = null;
    Texture2D pickedVirusDown      = null;
    Texture2D pickedVirusLeft      = null;

    
    ////////////////////////////////////////////////////////////////

    public override void Start()
    {
        List<Texture2D> virusTextures = new List<Texture2D>();
        for (int i = 0; i < GameManager.Instance.settings.VirusGraphics.Count; i++)
        {
            virusTextures.Add(GameManager.Instance.settings.VirusGraphics[i]);
        }

        ////////////////////////////////////////////////////////////////
        // 1) Find Scene References (by name bc we are hacky af)
        
        GameObject displayObjectMiddle      = GameObject.Find("Matcher_Middle");
        GameObject displayObjectUp          = GameObject.Find("Matcher_Up");
        GameObject displayObjectRight       = GameObject.Find("Matcher_Right");
        GameObject displayObjectDown        = GameObject.Find("Matcher_Down");
        GameObject displayObjectLeft        = GameObject.Find("Matcher_Left");

        ////////////////////////////////////////////////////////////////
        // 2) Setup Images
        // * We pick the virus to be the target of the game
        // * Then we pick the virus for the side options. Everytime we decided on one type of virus for a side display, we remove it from the options fpr the next one.

        //Pick a virus to be the middle target
        m_IndexMiddle                   = Random.Range(0, virusTextures.Count);
        pickedVirusTarget               = virusTextures[m_IndexMiddle];
        
        MeshRenderer rendererOfTarget = displayObjectMiddle.GetComponent<MeshRenderer>();
        rendererOfTarget.material.SetTexture("_MainTex", pickedVirusTarget);
        
        ////////////////////////////////////////////////////////////////

        //Pick a virus to be the "up" option
        m_IndexUp                       = Random.Range(0, virusTextures.Count);
        pickedVirusUp                   = virusTextures[m_IndexUp];
        MeshRenderer rendererOfUp       = displayObjectUp.GetComponent<MeshRenderer>();
        rendererOfUp.material.SetTexture("_MainTex", pickedVirusUp);

        virusTextures.Remove(pickedVirusUp);

        //Pick a virus to be the "right" option
        m_IndexRight                    = Random.Range(0, virusTextures.Count);
        pickedVirusRight                = virusTextures[m_IndexRight];
        MeshRenderer rendererOfRight    = displayObjectRight.GetComponent<MeshRenderer>();
        rendererOfRight.material.SetTexture("_MainTex", pickedVirusRight);

        virusTextures.Remove(pickedVirusRight);

        //Pick a virus to be the "down" option
        m_IndexDown                     = Random.Range(0, virusTextures.Count);
        pickedVirusDown                 = virusTextures[m_IndexDown];
        MeshRenderer rendererOfDown     = displayObjectDown.GetComponent<MeshRenderer>();
        rendererOfDown.material.SetTexture("_MainTex", pickedVirusDown);

        virusTextures.Remove(pickedVirusDown);

        //Pick a virus to be the "left" option
        m_IndexLeft                     = Random.Range(0, virusTextures.Count);
        pickedVirusLeft                 = virusTextures[m_IndexLeft];
        MeshRenderer rendererOfLeft     = displayObjectLeft.GetComponent<MeshRenderer>();
        rendererOfLeft.material.SetTexture("_MainTex", pickedVirusLeft);

        //Debug.Log("Target: " + pickedVirusTarget.name);
        //Debug.Log(pickedVirusUp.name);
        //Debug.Log(pickedVirusRight.name);
        //Debug.Log(pickedVirusDown.name);
        //Debug.Log(pickedVirusLeft.name);
    }

    ////////////////////////////////////////////////////////////////

    public override void Finish()
    {
        return;
    }
    ////////////////////////////////////////////////////////////////

    public override MinigameTickResult Tick()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return GuessTop();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return GuessRight();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return GuessDown();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return GuessLeft();
        }

        return MinigameTickResult.ContinueTick;
    }
    
    MinigameTickResult GuessTop()
    {
        if (pickedVirusTarget == pickedVirusUp)
        {
            return MinigameTickResult.EarlySuccess;
        }
        else
        {
            return MinigameTickResult.Failed;
        }
    }

    MinigameTickResult GuessRight()
    {
        if (pickedVirusTarget == pickedVirusRight)
        {
            return MinigameTickResult.EarlySuccess;
        }
        else
        {
            return MinigameTickResult.Failed;
        }
    }

    MinigameTickResult GuessDown()
    {
        if (pickedVirusTarget == pickedVirusDown)
        {
            return MinigameTickResult.EarlySuccess;
        }
        else
        {
            return MinigameTickResult.Failed;
        }
    }

    MinigameTickResult GuessLeft()
    {
        if (pickedVirusTarget == pickedVirusLeft)
        {
            return MinigameTickResult.EarlySuccess;
        }
        else
        {
            return MinigameTickResult.Failed;
        }
    }
}
