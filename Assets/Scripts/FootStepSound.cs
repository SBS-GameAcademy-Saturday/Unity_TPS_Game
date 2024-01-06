using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
	private AudioSource audioSource;

	[Header("FootStep Sources")]
	[SerializeField] private AudioClip[] footstepsSound;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();	
	}

	public void Step()
	{
		AudioClip clip = GetRandomFootStep();
		audioSource.PlayOneShot(clip);
	}

	private AudioClip GetRandomFootStep()
	{
		return footstepsSound[UnityEngine.Random.Range(0, footstepsSound.Length)];
	}
}
