using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTurnOff : MonoBehaviour
{
	[Header("Generator Lights and Button")]
	public GameObject greenLight;
	public GameObject redLight;
	public bool button;


	[Header("Generator Sound Effects and Radius")]
	private float radius = 2f;
	public PlayerController player;
	public Animator animator;
	public AudioSource GeneratorAudioSource;
	[Header("Sounds")]
	public AudioClip objectiveCompoletedSound;
	public AudioSource audioSource;


	private void Awake()
	{
		button = false;
		audioSource = GetComponent<AudioSource>();
	}
	// Update is called once per frame
	void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && Vector3.Distance(transform.position,player.transform.position) < radius)
		{
			audioSource.PlayOneShot(objectiveCompoletedSound);
			button = true;
			animator.enabled = false;
			greenLight.SetActive(false);
			redLight.SetActive(true);
			GeneratorAudioSource.Stop();
			ObjectivesComplete.Instance.GetObjectives(true, true, true, false);

		}
		else if(button == false)
		{
			greenLight.SetActive(true);
			redLight.SetActive(false);
			//audioSource.Stop();
		}
    }
}
