using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MoveState : StateMachineBehaviour
{

    private NavMeshAgent agent;
    private IRunner runner;
    private float timer;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        runner = animator.gameObject.GetComponent<IRunner>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (runner.onGroundID == 0)
        {
            if (agent.remainingDistance < (agent.stoppingDistance + 0.1f))
            {
                agent.SetDestination(FindNextDestination());
            }
        }
        else if (runner.onGroundID == 1)
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                timer = 0;

                switch (Random.Range(0, 4))
                {
                    case 0:
                        agent.Move(-animator.transform.forward);
                        break;
                    case 1:
                        agent.Move(animator.transform.right);
                        break;
                    case 2:
                        agent.Move(-animator.transform.right);
                        break;
                    default:
                        agent.Move(animator.transform.forward);
                        break;
                }
            }
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