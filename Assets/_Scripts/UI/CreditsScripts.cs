using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class CreditsScripts : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene((int)GameManager.Scenes.MainMenu);
		}
	}
}
