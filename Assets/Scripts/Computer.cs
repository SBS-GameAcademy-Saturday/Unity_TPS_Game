using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Computer : MonoBehaviour
{
	[Header("Cumputer On/Off")]
	public bool lightOn = true;
	private float radius = 2.5f;
	public Light lights;

	[Header("Computer Assign Things")]
	public PlayerController player;
	[SerializeField] private GameObject ComputerUI;
	[SerializeField] private int showComputerUIfor = 5;

	[Header("Sounds")]
	public AudioClip objectiveCompoletedSound;
	public AudioSource audioSource;

	private void Update()
	{
		if(Vector3.Distance(transform.position,player.transform.position) < radius)
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				StartCoroutine(ShowComputerUI());
				lightOn = false;
				lights.intensity = 0;
				//objective completed
				ObjectivesComplete.Instance.GetObjectives(true, true, false, false);
				//Sound Effect
				audioSource.PlayOneShot(objectiveCompoletedSound);
			}
		}
	}

	IEnumerator ShowComputerUI()
	{
		ComputerUI.SetActive(true);
		yield return new WaitForSeconds(showComputerUIfor);
		ComputerUI.SetActive(false);
	}

}
