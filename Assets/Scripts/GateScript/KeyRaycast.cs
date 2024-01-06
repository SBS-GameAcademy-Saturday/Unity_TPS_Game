using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace KeyNetwork
{
	public class KeyRaycast : MonoBehaviour
	{
		[Header("Raycast Radius and Layer")]
		[SerializeField] private int rayRadius = 6;
		[SerializeField] private LayerMask LayerMaskCollective;
		[SerializeField] private string banLayerName = null;

		private KeyObjectRegulator raycastedObject;
		[SerializeField] private KeyCode openGateButton = KeyCode.F;
		[SerializeField] private Image crosshair = null;

		private bool checkCrosshair;
		private bool OneTime;

		private string collectiveTag = "CollectiveObject";

		private void Update()
		{
			RaycastHit hitInfo;

			Vector3 forwardDirection = transform.TransformDirection(Vector3.forward);

			int mask = 1 << LayerMask.NameToLayer(banLayerName) | LayerMaskCollective.value;

			if(Physics.Raycast(transform.position,forwardDirection,out hitInfo, rayRadius, mask))
			{
				if (hitInfo.collider.CompareTag(collectiveTag))
				{
					if (!OneTime)
					{
						raycastedObject = hitInfo.collider.gameObject.GetComponent<KeyObjectRegulator>();
						ChangeCrosshair(true);
					}

					checkCrosshair = true;
					OneTime = true;

					if (Input.GetKeyDown(openGateButton))
					{
						raycastedObject.FoundObject();
					}
				}
			}
			else
			{
				if (checkCrosshair)
				{
					ChangeCrosshair(false);
					OneTime = false;
				}
			}
		}

		private void ChangeCrosshair(bool change)
		{
			if(change && !OneTime)
			{
				crosshair.color = Color.blue;
			}
			else
			{
				crosshair.color = Color.white;
				checkCrosshair = false;
			}
		}
	}
}