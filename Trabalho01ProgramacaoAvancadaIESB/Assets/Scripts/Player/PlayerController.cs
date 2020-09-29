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
	private float speed = 0;

	private void Awake()
	{
		runnerID = PlayerParameters.RunnerID;
		playerRigidbody = this.GetComponent<Rigidbody>();
		agent = this.GetComponent<NavMeshAgent>();
		maxSpeed = PlayerParameters.MaxVelocity;
		rotationSpeed = PlayerParameters.RotationSpeed;
		acelerationForce = PlayerParameters.AcelerationForce;
		GameManager.Instance.runners.Add(this.GetComponent<IRunner>());
	}

	private void Update()
	{
		inputVertical = Input.GetAxis("Vertical");
		inputHorizontal = Input.GetAxis("Horizontal");
		RecalculateSpeed();
		Walk();

		// if (WrongDirection())
		// {
		// 	wrongDirectionWarning.SetActive(true);
		// }
		// else
		// {
		// 	wrongDirectionWarning.SetActive(false);
		// }

		if (nextCheckPoint > 5)
		{
			CompleteLap();
		}

	}

	private void FixedUpdate()
	{
		if (!isStunned)
		{
			Move();
			Rotate();
		}
	}

	public void RecalculateSpeed()
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
			speed = (maxSpeed / NavMesh.GetAreaCost(index));
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
			if (playerRigidbody.velocity.magnitude < speed)
			{
				playerRigidbody.AddForce(acelerationForce * transform.forward);
			}
		}
		if (inputVertical < 0f)
		{
			if (playerRigidbody.velocity.magnitude < (speed / 2f))
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