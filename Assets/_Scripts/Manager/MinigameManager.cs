using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager
{
    private List<BaseMinigame> m_CurrentMinigames;
    
    public void StartMinigame(BaseMinigame minigame)
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
            SequenceMinigame sequenceMinigame = new SequenceMinigame();
            sequenceMinigame.Setup(null, 0);

            StartMinigame(sequenceMinigame);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ScrewdriverMinigame screwdriverMinimap = new ScrewdriverMinigame();
            screwdriverMinimap.Setup(null, 0);

            StartMinigame(screwdriverMinimap);
        }

    }

    ////////////////////////////////////////////////////////////////
}
