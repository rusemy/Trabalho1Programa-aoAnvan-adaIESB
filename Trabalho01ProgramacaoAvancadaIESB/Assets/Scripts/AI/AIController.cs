using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour, IRunner
{
    [SerializeField] private AIParameters aIParameters;
    public AIParameters AIParameters => aIParameters;

    [SerializeField] private NavMeshAgent agent;
    public int setRunnerID = 2;
    public int runnerID { get; set; }

    public int lapNumber { get; set; } = 0;
    public int nextCheckPoint { get; set; } = 0;
    public int onGroundID { get; set; }
    public PowerUp availablePowerUp { get; set; }
    public bool isStunned { get; set; } = false;
    public float maxSpeed { get; set; }
    public float timeToUsePowerUp;
    private float lastDistance;
    private float timer;
    private bool RaceHasStarted = false;

    void Awake()
    {
        runnerID = setRunnerID;
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

    private void OnEnable()
    {
        RaceHasStarted = false;
        GameManager.OnStartRace += StartRace;
    }

    private void OnDisable()
    {
        GameManager.OnStartRace -= StartRace;
    }

    void Update()
    {
        RecalculateSpeed();
        if (availablePowerUp != null)
        {
            timer += Time.deltaTime;

            if (timer > timeToUsePowerUp)
            {
                UsePowerUp();
                timer = 0;
            }
        }
        if (nextCheckPoint > 5)
        {
            CompleteLap();
        }
    }

    public void RecalculateSpeed()
    {
        if (RaceHasStarted)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, 1f, NavMesh.AllAreas))
            {
                int areaIndex = hit.mask;
                int index = 0;
                while ((areaIndex >>= 1) > 0)
                {
                    index++;
                }
                agent.speed = (maxSpeed / NavMesh.GetAreaCost(index));
            }
        }
        else
        {
            agent.speed = 0;
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

    public void StartRace()
    {
        RaceHasStarted = true;
        int randomPointIndex = Random.Range(0, 5);
        NavMeshHit hit;
        NavMesh.SamplePosition(GameManager.Instance.powerUpPoints[nextCheckPoint, randomPointIndex].position, out hit, 1.5f, NavMesh.AllAreas);
        agent.SetDestination(hit.position);
    }

}