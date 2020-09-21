using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour, IRunner
{
    [SerializeField] private NavMeshAgent agent;
    public int runnerID { get; set; }

    public int lapNumber { get; set; } = 0;
    public int nextCheckPoint { get; set; } = 0;
    private float lastDistance;

    void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        lastDistance = agent.remainingDistance;
        if (runnerID <= 1)
        {
            runnerID = 2;
        }

    }

    void Update()
    {
        if (nextCheckPoint > 5)
        {
            CompleteLap();
        }
    }

    public void CompleteLap()
    {
        nextCheckPoint = 0;
        GameManager.Instance.ranking[lapNumber, GameManager.Instance.numberOfPlayerThatCompletedLap[lapNumber]] = runnerID;
        GameManager.Instance.numberOfPlayerThatCompletedLap[lapNumber]++;
        lapNumber++;
        if (lapNumber > 2)
        {
            agent.speed = 0f;
        }
    }

    public bool WrongDirection()
    {
        var distance = agent.remainingDistance;
        if (distance > lastDistance)
        {
            lastDistance = distance;
            return true;
        }
        lastDistance = distance;
        return false;
    }

}