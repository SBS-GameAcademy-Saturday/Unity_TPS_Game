using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoCount : MonoBehaviour
{
	public TMP_Text ammunitionText;
	public TMP_Text magText;

	public static AmmoCount Instance;

	private void Awake()
	{
		Instance = this;
	}

	public void UpdateAmmoText(int presentAmmunition)
	{
		ammunitionText.text = "Ammo : " + presentAmmunition;
	}
	public void UpdateMagText(int mag)
	{
		magText.text = "Mag : " + mag;
	}
}
