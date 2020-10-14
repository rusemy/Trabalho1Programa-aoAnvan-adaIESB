using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator runnerAnimator;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private IRunner runner;
    [SerializeField] private float minTimeToAFK = 4f;
    [SerializeField] private float maxTimeToAFK = 6f;
    [SerializeField] private int numberOfAnimationsAFK = 2;

    private float timeToAFK;
    private float timer;
    private int animationAFK;

    private void Awake()
    {
        runnerAnimator = this.GetComponent<Animator>();
        runner = this.GetComponent<IRunner>();
        timeToAFK = Random.Range(minTimeToAFK, maxTimeToAFK);
        playerRigidbody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (runner.lapNumber > 2)
        {
            if (GameManager.Instance.ranking[2, 0] == runner.runnerID)
            {
                runnerAnimator.SetTrigger("Win");
            }
            else
            {
                runnerAnimator.SetTrigger("Lose");
            }
        }

        runnerAnimator.SetFloat("Velocity", playerRigidbody.velocity.magnitude);

        runnerAnimator.SetFloat("VelocityX", Input.GetAxis("Horizontal"));

        runnerAnimator.SetFloat("VelocityZ", (Mathf.Abs(playerRigidbody.velocity.z) * Input.GetAxis("Vertical")));

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