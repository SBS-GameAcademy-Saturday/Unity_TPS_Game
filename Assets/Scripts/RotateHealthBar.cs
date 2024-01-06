using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHealthBar : MonoBehaviour
{
	public Transform MainCam;

	private void LateUpdate()
	{
		transform.LookAt(MainCam);
	}
}
