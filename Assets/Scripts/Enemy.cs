using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	[Header("Enemy Health and Damage")]
	private float EnemyHealth = 100f;
	private float presentHealth;
	public float Damage = 5;
	public HealthBar healthBar;

	[Header("Enemy Things")]
	public NavMeshAgent EnemyAgent;
	public Transform LookPoint;
	public Camera ShootingRaycastArea;
	public Transform playerBody;
	public LayerMask PlayerLayer;


	[Header("Enemy Guarding Var")]
	public GameObject[] walkPoints;
	int currentEnemyPosition = 0;
	public float enemySpeed;
	float walkingPointRadius = 2;


	[Header("Enemy Shooting var")]
	public float timebtwShoot;
	bool previouslyShoot;


	[Header("Sound And UI")]
	public AudioClip shootingSound;
	public AudioSource audioSource;


	[Header("Enemy Animation and Spark Effect")]
	public Animator animator;
	public ParticleSystem muzzleSpark;

	[Header("Enemy Mood/Situation")]
	public float visionRadius;
	public float shootingRaidus;
	public bool playerInvisionRadius;
	public bool playerInShootingRadius;

	private void Awake()
	{
		presentHealth = EnemyHealth;
		playerBody = GameObject.Find("Player").transform;
		EnemyAgent = GetComponent<NavMeshAgent>();
		audioSource = GetComponent<AudioSource>();
		healthBar.GiveFullHealth();
	}

	private void Update()
	{
		playerInvisionRadius = Physics.CheckSphere(transform.position,visionRadius, PlayerLayer);
		playerInShootingRadius = Physics.CheckSphere(transform.position, shootingRaidus, PlayerLayer);

		if (!playerInvisionRadius && !playerInShootingRadius) Guard();
		if (playerInvisionRadius && !playerInShootingRadius) PursuePlayer();
		if (playerInvisionRadius && playerInShootingRadius) ShootPlayer();
	}

	private void Guard()
	{
		if (Vector3.Distance(walkPoints[currentEnemyPosition].transform.position,transform.position) < walkingPointRadius)
		{
			currentEnemyPosition = UnityEngine.Random.Range(0,walkPoints.Length);
			if(currentEnemyPosition >= walkPoints.Length)
			{
				currentEnemyPosition = 0;
			}
		}
		//transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentEnemyPosition].transform.position, Time.deltaTime * enemySpeed);

		//transform.LookAt(walkPoints[currentEnemyPosition].transform.position);
		EnemyAgent.SetDestination(walkPoints[currentEnemyPosition].transform.position);
	}

	private void PursuePlayer()
	{
		if (EnemyAgent.SetDestination(playerBody.position))
		{
			//aniamtion
			animator.SetBool("Walk",false);
			animator.SetBool("AimRun",true);
			animator.SetBool("Shoot",false);
			animator.SetBool("Die",false);

			//+ vision and shooting radius
			visionRadius = 30;
			shootingRaidus = 10;
		}
		else
		{
			animator.SetBool("Walk", false);
			animator.SetBool("AimRun", false);
			animator.SetBool("Shoot", false);
			animator.SetBool("Die", true);
		}
	}

	private void ShootPlayer()
	{
		EnemyAgent.SetDestination(transform.position);

		transform.LookAt(LookPoint);

		if (!previouslyShoot)
		{
			muzzleSpark.Play();
			audioSource.PlayOneShot(shootingSound);
			RaycastHit hit;

			if(Physics.Raycast(ShootingRaycastArea.transform.position,ShootingRaycastArea.transform.forward,out hit, shootingRaidus))
			{
				Debug.Log("Shooting " + hit.transform.name);

				PlayerController player = hit.transform.GetComponent<PlayerController>();
				if (player)
				{
					player.HitDamage(Damage);
				}

				animator.SetBool("Walk", false);
				animator.SetBool("AimRun", false);
				animator.SetBool("Shoot", true);
				animator.SetBool("Die", false);
			}

			previouslyShoot = true;
			Invoke(nameof(ActiveShooting), timebtwShoot);
		}
	}

	private void ActiveShooting()
	{
		previouslyShoot = false;
	}

	public void HitDamage(float takeDamage)
	{
		//+ vision and shooting radius
		visionRadius = 30;
		shootingRaidus = 10;

		presentHealth -= takeDamage;
		healthBar.SetHealth(presentHealth);
		if (presentHealth <= 0)
		{
			animator.SetBool("Walk", false);
			animator.SetBool("AimRun", false);
			animator.SetBool("Shoot", false);
			animator.SetBool("Die", true);

			Die();
		}
	}

	private void Die()
	{
		EnemyAgent.SetDestination(transform.position);
		EnemyAgent.isStopped = true;
		enemySpeed = 0;
		shootingRaidus = 0;
		visionRadius = 0;
		playerInvisionRadius = false;
		playerInShootingRadius = false;
		Destroy(gameObject,5.0f);
	}
}
