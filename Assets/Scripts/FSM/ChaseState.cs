using UnityEngine;
using UnityEngine.AI;

public class ChaseState : MonoBehaviour, IState
{
    private Animator animator;
    private NavMeshAgent EnemyAgent;

    private Transform target;
    private Enemy _enemy;
    public void EnterState(Enemy enemy)
    {
        if (!animator) animator = GetComponent<Animator>();
        if (!EnemyAgent) EnemyAgent = GetComponent<NavMeshAgent>();
       
        target = enemy.playerBody;
        _enemy = enemy;
        //aniamtion
        animator.SetBool("Walk", false);
        animator.SetBool("AimRun", true);
        animator.SetBool("Shoot", false);
        animator.SetBool("Die", false);
    }

    public void UpdateState()
    {
        if (EnemyAgent.SetDestination(target.position))
        {
        }
        else
        {
            
        }
    }
    public void ExitState()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("AimRun", false);
        animator.SetBool("Shoot", false);
        animator.SetBool("Die", false);
    }
}