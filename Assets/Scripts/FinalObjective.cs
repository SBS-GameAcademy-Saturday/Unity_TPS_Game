using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalObjective : MonoBehaviour
{
	[Header("Vehicle Button")]
	public KeyCode vehicleButton = KeyCode.K;


	[Header("Generator Sound Effects and Radius")]
	private float radius = 3;
	public PlayerController player;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(vehicleButton) && Vector3.Distance(transform.position, player.transform.position) < radius)
		{
			Time.timeScale = 1;
			ObjectivesComplete.Instance.GetObjectives(true, true, true, true);
			SceneManager.LoadScene("EndGameMenuScene");
		}
	}
}
