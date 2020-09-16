using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIController : MonoBehaviour, IRunner
{
    public int runnerID{get; set;}
    private NavMeshAgent agent;
    private float lastDistance;
    public int lapNumber {get; set;} = 0;
    public int nextCheckPoint{get; set;} = 0;

    void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }
    private void Start() {
        lastDistance = agent.remainingDistance;
        
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
        lapNumber++;
        GameManager.Instance.numberOfPlayerThatCompletedLap[lapNumber]++;
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
