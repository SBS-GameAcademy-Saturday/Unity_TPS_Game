using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	public ProgressBar progressBar;

	public void GiveFullHealth()
	{
		progressBar.BarValue = 100;
	}

	public void SetHealth(float health)
	{
		progressBar.BarValue = health;
	}
}
