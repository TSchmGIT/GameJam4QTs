using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputHelper
{
    ////////////////////////////////////////////////////////////////
    
    public enum Keys
    {
        Up      = 0,
        Right   = 1,
        Down    = 2,
        Left    = 3,
        Action  = 4
    }

    ////////////////////////////////////////////////////////////////
    
    public static KeyCode GetKeyCode(int playerID, Keys key)
    {
        if (playerID == 0)
        {
            switch(key)
            {
                case Keys.Up:           return KeyCode.W;
                case Keys.Right:        return KeyCode.D;
                case Keys.Down:         return KeyCode.S;
                case Keys.Left:         return KeyCode.A;
                case Keys.Action:       return KeyCode.Space;
            }
        }
        else
        {
            switch(key)
            {
                case Keys.Up:           return KeyCode.UpArrow;
                case Keys.Right:        return KeyCode.RightArrow;
                case Keys.Down:         return KeyCode.DownArrow;
                case Keys.Left:         return KeyCode.LeftArrow;
                case Keys.Action:       return KeyCode.Return;
            }
        }

        Debug.LogError("Wrong key");

        return 0;
    }
}
