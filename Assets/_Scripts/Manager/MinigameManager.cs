using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager
{
    private List<BaseMinigame> m_CurrentMinigames;
    
    public void StartMinigame(MinigameDisplayComponent display, MinigameType type, int playerID)
    {
        switch (type)
        {
            case MinigameType.Sequence:
            
                SequenceMinigame sequenceMinigame = new SequenceMinigame();
                sequenceMinigame.Setup(null, new Rect(100, 100, 10, 10), 0);

                BeginMinigame(sequenceMinigame);
            
            break;
            case MinigameType.Scredriver:
            
                ScrewdriverMinigame screwdriverMinimap = new ScrewdriverMinigame();
                screwdriverMinimap.Setup(null, new Rect(200, 100, 10, 10), 0);

                BeginMinigame(screwdriverMinimap);

            break;
        }
    }

    void BeginMinigame(BaseMinigame minigame)
    {
        minigame.Start();
        m_CurrentMinigames.Add(minigame);
    }

    public void Init()
    {
        m_CurrentMinigames = new List<BaseMinigame>();
    }

    ////////////////////////////////////////////////////////////////

    public void Tick()
    {
        for (int i = 0; i < m_CurrentMinigames.Count; i++)
        {
            MinigameTickResult result = m_CurrentMinigames[i].Tick();

            if (result != MinigameTickResult.ContinueTick)
            {
                m_CurrentMinigames[i].Finish();
                m_CurrentMinigames.RemoveAt(i);
                i--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartMinigame(null, MinigameType.Sequence, 0);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartMinigame(null, MinigameType.Scredriver, 0);
        }

    }

    ////////////////////////////////////////////////////////////////
}
