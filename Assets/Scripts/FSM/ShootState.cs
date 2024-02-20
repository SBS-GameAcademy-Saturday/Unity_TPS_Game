using UnityEngine;
using UnityEngine.AI;

public class ShootState : MonoBehaviour, IState
{
    public Transform LookPoint;
    public ParticleSystem muzzleSpark;

    [Header("Sound And UI")]
    public AudioClip shootingSound;
    public AudioSource audioSource;

    [Header("Enemy Shooting var")]
    public float timebtwShoot;

    public Camera ShootingRaycastArea;
    public float Damage = 5;
    public float shootingRaidus;
    bool previouslyShoot;

    private Animator animator;
    private NavMeshAgent EnemyAgent;
    private Enemy _enemy;

    public void EnterState(Enemy enemy)
    {
        if (!animator) animator = GetComponent<Animator>();
        if (!EnemyAgent) EnemyAgent = GetComponent<NavMeshAgent>();
		if (!audioSource) audioSource = GetComponent<AudioSource>();
        _enemy = enemy;

        //aniamtion
        animator.SetBool("Walk", false);
        animator.SetBool("AimRun", false);
        animator.SetBool("Shoot", true);
        animator.SetBool("Die", false);
    }
    public void UpdateState()
    {
        EnemyAgent.SetDestination(transform.position);

        transform.LookAt(LookPoint);

        if (!previouslyShoot)
        {
            muzzleSpark.Play();
            audioSource.PlayOneShot(shootingSound);
            RaycastHit hit;

            if (Physics.Raycast(ShootingRaycastArea.transform.position, ShootingRaycastArea.transform.forward, out hit, shootingRaidus))
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

    public void ExitState()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("AimRun", false);
        animator.SetBool("Shoot", false);
        animator.SetBool("Die", false);
    }
}