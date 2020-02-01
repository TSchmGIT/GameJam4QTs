using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameFactory
{
    public static BaseMinigame CreateMinigame(MinigameType type)
    {
        switch (type)
        {
            case MinigameType.Sequence:
                return new SequenceMinigame();
            default:
                Debug.LogError("Not implemented yet");
                break;
        }

        return null;
    }
}
