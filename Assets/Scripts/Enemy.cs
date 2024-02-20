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

	public HealthBar healthBar;

	[Header("Enemy Things")]
	public NavMeshAgent EnemyAgent;

	public Camera ShootingRaycastArea;
	public Transform playerBody;
	public LayerMask PlayerLayer;

	[Header("Enemy Animation and Spark Effect")]
	public Animator animator;
	public ParticleSystem muzzleSpark;

	[Header("Enemy Mood/Situation")]
	public float visionRadius;
	public float shootingRaidus;
	public bool playerInvisionRadius;
	public bool playerInShootingRadius;

	[Header("Enemy States")]
	[SerializeField] private PatrolState patrolState;
	[SerializeField] private ChaseState chaseState;
	[SerializeField] private ShootState shootState;
	private EnemyStateContext enemyStateContext;



	private void Awake()
	{
		enemyStateContext = new EnemyStateContext(this);
		presentHealth = EnemyHealth;
		playerBody = GameObject.Find("Player").transform;
		healthBar.GiveFullHealth();

		enemyStateContext.Transition(patrolState);
	}

	private void Update()
	{
		playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
		playerInShootingRadius = Physics.CheckSphere(transform.position, shootingRaidus, PlayerLayer);

		if (!playerInvisionRadius && !playerInShootingRadius) UpdateState(EState.Patrol);
		if (playerInvisionRadius && !playerInShootingRadius) UpdateState(EState.Chase);
		if (playerInvisionRadius && playerInShootingRadius) UpdateState(EState.Shoot);

		enemyStateContext.CurrentState.UpdateState();
	}

	private void UpdateState(EState state)
	{
		switch (state)
		{
			case EState.Patrol:
				enemyStateContext.Transition(patrolState);
				break;
			case EState.Chase:
				enemyStateContext.Transition(chaseState);
				break;
			case EState.Shoot:
				enemyStateContext.Transition(shootState);
				break;
		}
	}

	public void HitDamage(float takeDamage)
	{
		//+ vision and shooting radius
		//visionRadius = 30;

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
		shootingRaidus = 0;
		visionRadius = 0;
		playerInvisionRadius = false;
		playerInShootingRadius = false;
		Destroy(gameObject, 5.0f);
	}
}
