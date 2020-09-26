using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour, IRunner
{
    [SerializeField] private AIParameters aIParameters;
    public AIParameters AIParameters => aIParameters;

    [SerializeField] private NavMeshAgent agent;
    public int runnerID { get; set; }

    public int lapNumber { get; set; } = 0;
    public int nextCheckPoint { get; set; } = 0;
    public PowerUp availablePowerUp { get; set; }
    public bool isStunned { get; set; } = false;
    public float maxSpeed { get; set; }
    public float timeToUsePowerUp;
    private float lastDistance;
    private float timer;

    void Awake()
    {
        runnerID = AIParameters.RunnerID;
        agent = this.GetComponent<NavMeshAgent>();
        maxSpeed = AIParameters.MaxVelocity;
        GameManager.Instance.runners.Add(this);
    }
    private void Start()
    {
        lastDistance = agent.remainingDistance;
        if (runnerID <= 1)
        {
            runnerID = 2;
        }
        agent.speed = maxSpeed;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (availablePowerUp != null)
        {

            if (timer > timeToUsePowerUp)
            {
                UsePowerUp();
            }
        }
        agent.speed = maxSpeed;
        if (nextCheckPoint > 5)
        {
            CompleteLap();
        }
    }

    public void UsePowerUp()
    {
        StartCoroutine(availablePowerUp.Effect(this, this.transform));
        availablePowerUp = null;
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