using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectivesComplete : MonoBehaviour
{
	[Header("Objective To Compolete")]
	public TMP_Text Objective1;
	public TMP_Text Objective2;
	public TMP_Text Objective3;
	public TMP_Text Objective4;

	public static ObjectivesComplete Instance;

	private void Awake()
	{
		Init();
	}
	public void Init()
	{
		Instance = this;
	}

	public void GetObjectives(bool obj1, bool obj2, bool obj3, bool obj4)
	{
		if (obj1)
		{
			Objective1.text = "1, Key Picked Up";
			Objective1.color = Color.green;
		}
		//else
		//{
		//	Objective1.text = "1, Find Key to Open The Gate";
		//	Objective1.color = Color.white;
		//}

		if (obj2)
		{
			Objective2.text = "2, Computer is offline";
			Objective2.color = Color.green;
		}
		//else
		//{
		//	Objective2.text = "2, Shutdown the Cumputer System";
		//	Objective2.color = Color.white;
		//}

		if (obj3)
		{
			Objective3.text = "3, Generators is offline";
			Objective3.color = Color.green;
		}
		//else
		//{
		//	Objective3.text = "3, Shutdown both of the Generators";
		//	Objective3.color = Color.white;
		//}

		if (obj4)
		{
			Objective4.text = "4, Mission Completed";
			Objective4.color = Color.green;
		}
		//else
		//{
		//	Objective4.text = "4. Escape from the Faility";
		//	Objective4.color = Color.white;
		//}
	}

}
