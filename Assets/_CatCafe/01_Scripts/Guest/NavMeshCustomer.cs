using UnityEngine;
using UnityEngine.AI;

public class NavMeshCustomer : MonoBehaviour
{
    protected enum CustomerState
    {
        IDLE,
        WALK,
        REQUEST,
        CAT
    }

    [SerializeField]
    protected Transform target;

    // private Transform target;
    protected NavMeshAgent agent;
    protected CustomerState state;
    private float timer;
    public float radius;
    public float minWaitTime = 1f;
    public float maxWaitTime = 5f;

    protected virtual void Start()
    {
        if (target == null)
        {
            target = GameObject.FindWithTag("Furniture").transform;
        }
        agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(target.transform.position);
        timer = Random.Range(minWaitTime, maxWaitTime);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // if (agent.remainingDistance >= 5f)
        // {
        //     state = CustomerState.WALK;
        //     // Debug.Log("Customer has stopped");
        // }
        // else if(agent.remainingDistance < 5f && state == CustomerState.WALK)
        // {
        //     agent.isStopped = true;
        //     state = CustomerState.IDLE;
        // }

        timer -= Time.deltaTime;

        if (
            timer <= 0
            && agent.pathStatus == NavMeshPathStatus.PathComplete
            && agent.remainingDistance <= agent.stoppingDistance
        )
        {
            WanderRandomly();
            timer = Random.Range(minWaitTime, maxWaitTime);
        }
    }

    void WanderRandomly()
    {
        Debug.Log("Customer is wandering!");
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        randomDirection.y = transform.position.y; // Shift random point to the agent's current position

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius * 2, NavMesh.AllAreas))
        {
            Vector3 finalPosition = hit.position;
            Debug.Log($"Setting new position: {finalPosition}");
            agent.SetDestination(finalPosition);
            state = CustomerState.WALK; // Update state to WALK
        }
        else
        {
            Debug.Log("Failed to find valid position");
        }
    }
}
