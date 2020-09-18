using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationController : MonoBehaviour
{
    private Animator animatorAI;
    private UnityEngine.AI.NavMeshAgent agent;
    private float timeToAFK;
    private float timer;
    private int animationAFK;
    
    private void Awake() {
        animatorAI = this.GetComponent<Animator>();
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        timeToAFK = Random.Range(4f,6f);
    }

    void Update()
    {
        animatorAI.SetFloat("Velocity", agent.velocity.magnitude);

        animatorAI.SetFloat("VelocityX", agent.velocity.x);

        animatorAI.SetFloat("VelocityZ", agent.velocity.z);

        if(animatorAI.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            timer += Time.deltaTime;

            if (timer > timeToAFK)
            {
                animationAFK = Random.Range(0,2);
                if (animationAFK == 0)
                {
                    animatorAI.SetTrigger("AFK1");
                }
                else
                {
                    animatorAI.SetTrigger("AFK2");
                }
            }
        }
    }
}
