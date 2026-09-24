using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class limpadormovimento : MonoBehaviour
{
    public enum State { Patrol, Chase, Search }

    [Header("Referências")]
    public Transform player;
    public Transform[] patrolPoints;

    [Header("Detecção")]
    public float visionRange = 12f;
    [Range(0, 360)] public float visionAngle = 90f;
    public float hearingRange = 5f;
    public LayerMask obstacleMask;
    public LayerMask playerMask;

    [Header("Movimento")]
    public float patrolSpeed = 2.5f;
    public float chaseSpeed = 4.5f;
    public float patrolWaitTime = 2f;

    [Header("Busca")]
    public float searchDuration = 5f;
    public float searchLookSpeed = 90f;

    [Header("Debug")]
    public bool drawGizmos = true;

    private NavMeshAgent agent;
    private State currentState = State.Patrol;
    private Vector3 lastKnownPosition;
    private int currentPatrolIndex = 0;
    private float waitTimer = 0f;
    private float searchTimer = 0f;
    private bool waitingAtPoint = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        agent.speed = patrolSpeed;

        if (patrolPoints == null || patrolPoints.Length == 0)
            GoToRandomPointInMaze();
        else
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    void Update()
    {
        if (player == null) return;

        switch (currentState)
        {
            case State.Patrol: UpdatePatrol(); break;
            case State.Chase: UpdateChase(); break;
            case State.Search: UpdateSearch(); break;
        }
    }

    void UpdatePatrol()
    {
        if (CanSeePlayer() || IsPlayerTooClose())
        {
            EnterChase();
            return;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!waitingAtPoint)
            {
                waitingAtPoint = true;
                waitTimer = patrolWaitTime;
            }
            else
            {
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0f)
                {
                    waitingAtPoint = false;
                    NextPatrolTarget();
                }
            }
        }
    }

    void NextPatrolTarget()
    {
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.speed = patrolSpeed;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
        else
        {
            GoToRandomPointInMaze();
        }
    }

    void GoToRandomPointInMaze()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 15f;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 15f, NavMesh.AllAreas))
        {
            agent.speed = patrolSpeed;
            agent.SetDestination(hit.position);
        }
    }

    void EnterChase()
    {
        currentState = State.Chase;
        agent.speed = chaseSpeed;
    }

    void UpdateChase()
    {
        if (CanSeePlayer() || IsPlayerTooClose())
        {
            lastKnownPosition = player.position;
            agent.SetDestination(player.position);
        }
        else
        {
            EnterSearch();
        }
    }

    void EnterSearch()
    {
        currentState = State.Search;
        searchTimer = searchDuration;
        agent.speed = patrolSpeed;
        agent.SetDestination(lastKnownPosition);
    }

    void UpdateSearch()
    {
        if (CanSeePlayer() || IsPlayerTooClose())
        {
            EnterChase();
            return;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            transform.Rotate(Vector3.up, searchLookSpeed * Time.deltaTime);
            searchTimer -= Time.deltaTime;

            if (searchTimer <= 0f)
            {
                currentState = State.Patrol;
                NextPatrolTarget();
            }
        }
    }

    bool CanSeePlayer()
    {
        Vector3 dirToPlayer = player.position - transform.position;
        float distance = dirToPlayer.magnitude;

        if (distance > visionRange) return false;

        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        if (angle > visionAngle * 0.5f) return false;

        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dirToPlayer.normalized,
            out RaycastHit hitInfo, distance, obstacleMask | playerMask))
        {
            bool hitIsPlayer = ((1 << hitInfo.collider.gameObject.layer) & playerMask) != 0;
            return hitIsPlayer;
        }

        return true;
    }

    bool IsPlayerTooClose()
    {
        return Vector3.Distance(transform.position, player.position) <= hearingRange;
    }

    void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hearingRange);

        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle * 0.5f, 0) * transform.forward * visionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle * 0.5f, 0) * transform.forward * visionRange;
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, leftBoundary);
        Gizmos.DrawRay(transform.position, rightBoundary);
    }
}
