using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStartGameScript : MonoBehaviour
{
    public void LoadGame()
	{
		GameManager.Instance.LoadGame();
	}
}
