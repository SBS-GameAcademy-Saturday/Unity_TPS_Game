using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
	[Header("Rifle Things")]
	public Camera cam;
	public float Damage = 10f;
	public float ShootingRange = 100f;
	public float fireCharge = 15f;
	public Animator animator;
	public PlayerController player;

	[Header("Rifle Ammunition and shooting")]
	private int maximumAmmunition = 20;
	private int mag = 15;
	private int presentAmmunition;
	public float reloadingTime = 1.3f;
	private bool setReloading = false;
	private float nextTimeToShoot = 0f;

	[Header("Rifle Effects")]
	public ParticleSystem muzzleSpark;
	public GameObject ImpackEffect;
	public GameObject GoreEffect;
	public GameObject DroneEffect;

	//[Header("Rifle Effect")]

	[Header("Sound And UI")]
	[SerializeField] private GameObject AmmoOutUI;
	[SerializeField] private int timeToShowUI = 1;
	public AudioClip shootingSound;
	public AudioClip reloadingSound;
	public AudioSource audioSource;


	// Update is called once per frame

	private void Awake()
	{
		presentAmmunition = maximumAmmunition;
	}

	void Update()
    {
		if (setReloading)
			return;

		if(presentAmmunition <= 0)
		{
			StartCoroutine(Reload());
			return;
		}

		if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
		{
			animator.SetBool("Fire", true);
			animator.SetBool("Idle", false);
			nextTimeToShoot = Time.time + 1 / fireCharge;
			Shoot();
		}
		else if(Input.GetButton("Fire1") == false)
		{
			animator.SetBool("Fire", false);
			animator.SetBool("Idle", true);
		}
    }

	private void Shoot()
	{
		//check for mag

		if(mag == 0)
		{
			//show ammo out text
			StartCoroutine(ShowAmmoOut());
			return;
		}

		presentAmmunition--;

		if (presentAmmunition == 0)
		{
			mag--;
		}

		AmmoCount.Instance.UpdateAmmoText(presentAmmunition);
		AmmoCount.Instance.UpdateMagText(mag);

		muzzleSpark.Play();
		audioSource.PlayOneShot(shootingSound);
		RaycastHit hitInfo;

		if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hitInfo, ShootingRange))
		{
			Debug.Log(hitInfo.collider.name);

			Damageable damageable = hitInfo.collider.GetComponent<Damageable>();
			Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
			EnemyDrone enemyDrone = hitInfo.collider.GetComponent<EnemyDrone>();

			if(damageable != null)
			{
				damageable.HitDamage(Damage);
				GameObject impackGO = Instantiate(ImpackEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
				Destroy(impackGO, 1f);
			}
			else if(enemy != null)
			{
				enemy.HitDamage(Damage);
				GameObject impackGO = Instantiate(GoreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
				Destroy(impackGO, 1f);
			}
			else if (enemyDrone != null)
			{
				enemyDrone.HitDamage(Damage);
				GameObject impackGO = Instantiate(DroneEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
				Destroy(impackGO, 2f);
			}
		}
	}

	IEnumerator Reload()
	{
		player.playerSpeed = 0;
		player.playerSprint = 0;
		setReloading = true;
		Debug.Log("Reloading...");
		animator.SetBool("Reloading", true);
		audioSource.PlayOneShot(reloadingSound);
		yield return new WaitForSeconds(reloadingTime);
		animator.SetBool("Reloading", false);
		presentAmmunition = maximumAmmunition;
		player.playerSpeed = 2;
		player.playerSprint = 5;
		setReloading = false;
	}

	IEnumerator ShowAmmoOut()
	{
		AmmoOutUI.SetActive(true);
		yield return new WaitForSeconds(timeToShowUI);
		AmmoOutUI.SetActive(false);
	}
}
