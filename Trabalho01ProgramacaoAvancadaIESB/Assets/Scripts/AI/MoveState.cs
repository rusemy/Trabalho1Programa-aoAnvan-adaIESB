using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MoveState : StateMachineBehaviour
{

    private NavMeshAgent agent;
    private IRunner runner;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        runner = animator.gameObject.GetComponent<IRunner>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance < (agent.stoppingDistance + 0.1f))
        {
            agent.SetDestination(FindNextDestination());
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    private Vector3 FindNextDestination()
    {
        int randomPointIndex = Random.Range(0, 5);
        NavMeshHit hit;

        NavMesh.SamplePosition(GameManager.Instance.powerUpPoints[runner.nextCheckPoint, randomPointIndex].position, out hit, 1.5f, NavMesh.AllAreas);
        return hit.position;
    }
}