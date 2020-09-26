using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour, IRunner
{
	[SerializeField] private PlayerParameters playerParameters;
	public PlayerParameters PlayerParameters => playerParameters;

	[SerializeField] private Rigidbody playerRigidbody;
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private GameObject wrongDirectionWarning;
	[SerializeField] private GameObject FinalScreen;

	public int runnerID { get; set; } = 1;
	public int lapNumber { get; set; } = 0;
	public int nextCheckPoint { get; set; } = 0;
	public bool isStunned { get; set; } = false;
	public float maxSpeed { get; set; }
	public PowerUp availablePowerUp { get; set; }

	private float inputVertical;
	private float inputHorizontal;
	private float lastDistance;
	private float rotationSpeed;
	private float acelerationForce;

	private void Awake()
	{
		runnerID = PlayerParameters.RunnerID;
		playerRigidbody = this.GetComponent<Rigidbody>();
		maxSpeed = PlayerParameters.MaxVelocity;
		rotationSpeed = PlayerParameters.RotationSpeed;
		acelerationForce = PlayerParameters.AcelerationForce;
		GameManager.Instance.runners.Add(this);
	}

	private void Update()
	{
		inputVertical = Input.GetAxis("Vertical");
		inputHorizontal = Input.GetAxis("Horizontal");

		Walk();

		// if (WrongDirection())
		// {
		// 	wrongDirectionWarning.SetActive(true);
		// }

	}

	private void FixedUpdate()
	{
		if (!isStunned)
		{
			Move();
			Rotate();
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
			maxSpeed = 0f;
			FinalScreen.SetActive(true);
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

	public void PathToNextCheckpoint()
	{
		NavMeshHit hit;
		NavMesh.SamplePosition(GameManager.Instance.checkPoints[nextCheckPoint].position, out hit, 1f, NavMesh.AllAreas);
		agent.CalculatePath(hit.position, agent.path);
	}

	private void Move()
	{
		if (inputVertical > 0f)
		{
			if (playerRigidbody.velocity.magnitude < maxSpeed)
			{
				playerRigidbody.AddForce(acelerationForce * transform.forward);
			}
		}
		if (inputVertical < 0f)
		{
			if (playerRigidbody.velocity.magnitude < (maxSpeed / 2f))
			{
				playerRigidbody.AddForce(acelerationForce * -transform.forward);
			}
		}
	}

	private void Walk()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			maxSpeed /= 2f;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			maxSpeed *= 2f;
		}
	}

	private void Rotate()
	{
		if (playerRigidbody.velocity.magnitude > 0.1f)
		{
			transform.Rotate(transform.up * inputHorizontal * rotationSpeed);
		}
	}

}