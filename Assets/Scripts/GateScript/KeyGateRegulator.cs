using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyNetwork
{
	public class KeyGateRegulator : MonoBehaviour
	{
		[Header("Animations")]
		private Animator gateAnimation;
		private bool OpenGate = false;
		[SerializeField] private string OpenAnimationName = "GateOpen";
		[SerializeField] private string CloseAnimationName = "GateClose";

		[Header("Time and UI")]
		[SerializeField] private int timeToShowUI = 1;
		[SerializeField] private GameObject showGateLockedUI = null;
		[SerializeField] private KeyList keyList = null;
		[SerializeField] private int waitTimer = 1;
		[SerializeField] private bool pauseInteraction = false;

		[Header("Sound Effect")]
		public AudioClip gateSound;
		public AudioSource audioSource;

		private void Awake()
		{
			gateAnimation = GetComponent<Animator>();

		}

		public void StartAnimation()
		{
			if (keyList.hasKey)
			{
				Open_Gate();
			}
			else
			{
				StartCoroutine(ShowGateLocked());
			}
		}
		private IEnumerator StopGateInteraction()
		{
			pauseInteraction = true;
			yield return new WaitForSeconds(waitTimer);
			pauseInteraction = false;
		}

		void Open_Gate()
		{
			if(!OpenGate && !pauseInteraction)
			{
				gateAnimation.Play(OpenAnimationName, 0, 0.0f);
				audioSource.PlayOneShot(gateSound);
				OpenGate = true;
				ObjectivesComplete.Instance.GetObjectives(true, false, false, false);
				StartCoroutine(StopGateInteraction());
			}
			else if(OpenGate && !pauseInteraction) 
			{
				gateAnimation.Play(CloseAnimationName, 0, 0.0f);
				audioSource.PlayOneShot(gateSound);
				OpenGate = false;
				StartCoroutine(StopGateInteraction());
			}
		}

		IEnumerator ShowGateLocked()
		{
			showGateLockedUI.SetActive(true);
			yield return new WaitForSeconds(timeToShowUI);
			showGateLockedUI.SetActive(false);
		}
	}
}