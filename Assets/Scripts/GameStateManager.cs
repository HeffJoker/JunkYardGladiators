using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class GameStateManager : MonoBehaviour {

	public Canvas GameOverGUI;
	public Canvas PauseGUI;
	public PlayerControl Player;
	public string LevelName;
	public float PauseCooldown = 0.5f;

	private bool gameOver = false;
	private bool paused = false;

    public bool isPaused()
    {
        return paused;
    }

	public void GameOver()
	{
		GameOverGUI.enabled = true;
		Player.enabled = false;
		gameOver = true;
	}

	public void Pause()
	{
		PauseGUI.enabled = true;
		StartCoroutine(DoPause());
	}

	public void UnPause()
	{
		paused = false;
		PauseGUI.enabled = false;
		Time.timeScale = 1;
	}
		
	public void StartGame()
	{
		gameOver = false;
		Score.Instance.Reset();
		Application.LoadLevel(LevelName);
	}

	void Update()
	{
		if(gameOver)
		{
			InputDevice inDevice = InputManager.ActiveDevice;

			if(inDevice != null && inDevice.MenuWasPressed)
				StartGame();

			if(Input.GetKeyUp(KeyCode.Return))
				StartGame();
		}
	}

	IEnumerator DoPause()
	{
		paused = true;
		Time.timeScale = 0;

		yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(PauseCooldown));

		InputDevice inDevice = InputManager.ActiveDevice;

		while(!inDevice.MenuWasPressed)
			yield return null;

		UnPause();
	}
}

public static class CoroutineUtil
{
	public static IEnumerator WaitForRealSeconds(float time)
	{
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + time)
		{
			yield return null;
		}
	}
}
