using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
	[Header("Camera to Assign")]
	public GameObject AimCam;
	public GameObject AimCanvas;
	public GameObject ThirdPersonCam;
	public GameObject ThirdPersonCanvas;

	[Header("Camera Animator")]
	public Animator animator;

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButton("Fire2"))
		{
			animator.SetBool("Aiming", true);

			ThirdPersonCam.SetActive(false);
			ThirdPersonCanvas.SetActive(false);
			AimCam.SetActive(true);
			AimCanvas.SetActive(true);
		}
		else
		{
			animator.SetBool("Aiming", false);

			ThirdPersonCam.SetActive(true);
			ThirdPersonCanvas.SetActive(true);
			AimCam.SetActive(false);
			AimCanvas.SetActive(false);
		}
    }
}
