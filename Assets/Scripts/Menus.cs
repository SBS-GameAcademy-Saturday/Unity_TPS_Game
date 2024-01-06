using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
	[Header("All Menus")]
	public GameObject PauseMenuUI;
	public GameObject EndGameMenuUI;
	public GameObject ObectivesMenuUI;

	public static bool GameIsStopped = false;

	private void Awake()
	{
		ObectivesMenuUI.GetComponent<ObjectivesComplete>().Init();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsStopped)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
		else if(Input.GetKeyDown(KeyCode.M))
		{
			if (GameIsStopped)
			{
				HideObjectives();
				Cursor.lockState = CursorLockMode.Locked;
			}
			else
			{
				ShowObjectives();
				Cursor.lockState = CursorLockMode.None;
			}
		}
	}

	public void ShowObjectives()
	{
		ObectivesMenuUI.SetActive(true);
		Time.timeScale = 0;
		GameIsStopped = true;
	}
	public void HideObjectives()
	{
		ObectivesMenuUI.SetActive(false);
		Time.timeScale = 1;
		GameIsStopped = false;
	}
	public void Resume()
	{
		PauseMenuUI.SetActive(false);
		Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Locked;
		GameIsStopped = false;
	}

	public void ReStart()
	{
		SceneManager.LoadScene("Mission");
	}

	public void LoadMenu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("Menu");
	}

	public void QuitGame()
	{
		Debug.Log("Quitting Game...");
		Application.Quit();
	}

	public void Pause()
	{
		PauseMenuUI.SetActive(true);
		Time.timeScale = 0;
		Cursor.lockState = CursorLockMode.None;
		GameIsStopped = true;
	}
}
