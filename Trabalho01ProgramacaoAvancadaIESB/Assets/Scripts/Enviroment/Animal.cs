using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<Transform> pointsToMove;
    [SerializeField] private float velocity;
    [SerializeField] private float minTimeToStop;
    [SerializeField] private float maxTimeToStop;

    private float timer = 0f;
    private float stopTime = 0f;
    private int actualPointIndex;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!agent.hasPath)
        {
            Move();
        }

        if (agent.remainingDistance < (agent.stoppingDistance + 0.1f))
        {
            timer += Time.deltaTime;

            if (timer >= stopTime)
            {
                Move();
            }
        }
    }

    private void Move()
    {
        timer = 0f;

        actualPointIndex++;
        if (actualPointIndex > pointsToMove.Count)
        {
            actualPointIndex = 0;
        }

        NavMeshHit hit;
        NavMesh.SamplePosition(pointsToMove[actualPointIndex].position, out hit, 1.5f, NavMesh.AllAreas);

        agent.SetDestination(hit.position);
    }

}