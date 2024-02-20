using UnityEngine;
using UnityEngine.AI;

public class PatrolState : MonoBehaviour, IState
{
    [Header("Enemy Guarding Var")]
    public GameObject[] walkPoints;
    int currentEnemyPosition = 0;

    float walkingPointRadius = 2;

    private Animator animator;
    private NavMeshAgent EnemyAgent;
    private Enemy _enemy;
    public void EnterState(Enemy enemy)
    {
        if (!animator) animator = GetComponent<Animator>();
        if (!EnemyAgent) EnemyAgent = GetComponent<NavMeshAgent>();

        _enemy = enemy;
        //aniamtion
        animator.SetBool("Walk", true);
        animator.SetBool("AimRun", false);
        animator.SetBool("Shoot", false);
        animator.SetBool("Die", false);
    }

    public void UpdateState()
    {
        if (Vector3.Distance(walkPoints[currentEnemyPosition].transform.position, transform.position) < walkingPointRadius)
        {
            currentEnemyPosition = UnityEngine.Random.Range(0, walkPoints.Length);
            if (currentEnemyPosition >= walkPoints.Length)
            {
                currentEnemyPosition = 0;
            }
        }
        //transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentEnemyPosition].transform.position, Time.deltaTime * enemySpeed);

        //transform.LookAt(walkPoints[currentEnemyPosition].transform.position);
        EnemyAgent.SetDestination(walkPoints[currentEnemyPosition].transform.position);
    }
    public void ExitState()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("AimRun", false);
        animator.SetBool("Shoot", false);
        animator.SetBool("Die", false);
    }

}