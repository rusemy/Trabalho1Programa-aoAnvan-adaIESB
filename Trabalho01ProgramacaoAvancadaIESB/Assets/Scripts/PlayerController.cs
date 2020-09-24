using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour, IRunner
{
	[SerializeField] private Rigidbody playerRigidbody;
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private GameObject wrongDirectionWarning;
	[SerializeField] private GameObject FinalScreen;
	[SerializeField] private float maxVelocity = 10f;
	[SerializeField] private float movementForce = 100f;
	[SerializeField] private float rotationSpeed = 15f;
	public int runnerID { get; set; } = 1;
	public int lapNumber { get; set; } = 0;
	public int nextCheckPoint { get; set; } = 0;

	private float inputVertical;
	private float inputHorizontal;
	private float lastDistance;

	private void Awake()
	{
		playerRigidbody = this.GetComponent<Rigidbody>();

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
		Move();
		Rotate();
	}

	public void CompleteLap()
	{
		nextCheckPoint = 0;
		GameManager.Instance.ranking[lapNumber, GameManager.Instance.numberOfPlayerThatCompletedLap[lapNumber]] = runnerID;
		GameManager.Instance.numberOfPlayerThatCompletedLap[lapNumber]++;
		lapNumber++;
		if (lapNumber > 2)
		{
			maxVelocity = 0f;
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
			if (playerRigidbody.velocity.magnitude < maxVelocity)
			{
				playerRigidbody.AddForce(movementForce * transform.forward);
			}
		}
		if (inputVertical < 0f)
		{
			if (playerRigidbody.velocity.magnitude < (maxVelocity / 2f))
			{
				playerRigidbody.AddForce(movementForce * -transform.forward);
			}
		}
	}

	private void Walk()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			maxVelocity /= 2f;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			maxVelocity *= 2f;
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