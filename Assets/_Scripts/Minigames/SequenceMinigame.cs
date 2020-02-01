using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceMinigame : BaseMinigame
{
    public MinigameDisplayComponent DisplayComponent => throw new System.NotImplementedException();

    ////////////////////////////////////////////////////////////////
    
    private List<KeyCode> m_OriginalSequence;
    private List<KeyCode> m_RemainingSequence;

    ////////////////////////////////////////////////////////////////

    public override void Start()
    {
        Debug.Log("Start Minigame Sequence");

        GenerateSequence();
        GenerateSequenceSprites();
    }

    ////////////////////////////////////////////////////////////////

    public override MinigameTickResult Tick()
    {
        MinigameTickResult result = AcceptInput();
        DisplaySequence();

        return result;
    }

    ////////////////////////////////////////////////////////////////

    public override void Finish()
    {
        // TODO
        Debug.Log("Finished Minigame Sequence");
    }

    ////////////////////////////////////////////////////////////////

    void GenerateSequence()
    {
        const int MIN_LENGTH = 3;
        const int MAX_LENGTH = 5;

        int length = Mathf.RoundToInt(Random.value * (MAX_LENGTH - MIN_LENGTH) + MIN_LENGTH);
        
        m_OriginalSequence = new List<KeyCode>();
        m_RemainingSequence = new List<KeyCode>();

        for (int i = 0; i < length; i++)
        {
            KeyCode keyCode = KeyCode.Alpha0;

            int keyDirection = Mathf.RoundToInt(Random.value * 3);
            switch (keyDirection)
            {
                case 0: keyCode = GetKeyCode(InputHelper.Keys.Up);      break;
                case 1: keyCode = GetKeyCode(InputHelper.Keys.Right);   break;
                case 2: keyCode = GetKeyCode(InputHelper.Keys.Down);    break;
                case 3: keyCode = GetKeyCode(InputHelper.Keys.Left);    break;
            }

            m_OriginalSequence.Add(keyCode);
            m_RemainingSequence.Add(keyCode);
        }
    }

    ////////////////////////////////////////////////////////////////
    
    MinigameTickResult AcceptInput()
    {
        if (m_RemainingSequence.Count == 0)
        {
            return MinigameTickResult.Failed;
        }

        KeyCode keyCode = m_RemainingSequence[0];

        bool anyKeyDown = AnyArrowKeyDown();
        if (!anyKeyDown)
        {
            return MinigameTickResult.ContinueTick;
        }

        if (Input.GetKeyDown(keyCode))
        {
            m_RemainingSequence.RemoveAt(0);
            
            if (m_RemainingSequence.Count == 0)
            {
                Debug.Log("You Win");
                return MinigameTickResult.EarlySuccess;
            }

            return MinigameTickResult.ContinueTick;
        }

        Debug.Log("You lose");
        return MinigameTickResult.Failed;
    }

    ////////////////////////////////////////////////////////////////
    
    void GenerateSequenceSprites()
    {
        // TODO
        for (int i = 0; i < m_RemainingSequence.Count; i++)
        {
            Debug.Log("Key " + m_OriginalSequence[i].ToString());
        }
    }

    ////////////////////////////////////////////////////////////////
    
    void DisplaySequence()
    {   
        // TODO
    }
}
