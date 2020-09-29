using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAnimationController : MonoBehaviour
{
    [SerializeField] private Animator runnerAnimator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private IRunner runner;
    [SerializeField] private float minTimeToAFK = 4f;
    [SerializeField] private float maxTimeToAFK = 6f;
    [SerializeField] private int numberOfAnimationsAFK = 2;

    private float timeToAFK;
    private float timer;
    private int animationAFK;
    private bool finish = false;

    private void Awake()
    {
        runnerAnimator = this.GetComponent<Animator>();
        runner = this.GetComponent<IRunner>();
        timeToAFK = Random.Range(minTimeToAFK, maxTimeToAFK);
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (runner.lapNumber > 2 && !finish)
        {
            if (GameManager.Instance.ranking[2, 0] == runner.runnerID)
            {
                runnerAnimator.SetTrigger("Win");
                finish = true;
            }
            else
            {
                runnerAnimator.SetTrigger("Lose");
                finish = true;
            }
        }
        runnerAnimator.SetFloat("Velocity", agent.velocity.magnitude);

        runnerAnimator.SetFloat("VelocityX", Vector3.Project(agent.velocity, transform.right).magnitude);

        runnerAnimator.SetFloat("VelocityZ", Vector3.Project(agent.velocity, transform.forward).magnitude);

        if (runnerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            timer += Time.deltaTime;

            if (timer > timeToAFK)
            {
                animationAFK = Random.Range(0, numberOfAnimationsAFK);
                if (animationAFK == 0)
                {
                    runnerAnimator.SetTrigger("AFK1");
                    timeToAFK = Random.Range(minTimeToAFK, maxTimeToAFK);
                    timer = 0;
                }
                else
                {
                    runnerAnimator.SetTrigger("AFK2");
                    timeToAFK = Random.Range(minTimeToAFK, maxTimeToAFK);
                    timer = 0;
                }
            }
        }
    }
}