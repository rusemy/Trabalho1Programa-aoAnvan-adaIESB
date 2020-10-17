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
	[SerializeField] private GameObject wrongDirectionWarning;
	[SerializeField] private GameObject FinalScreen;

	public int runnerID { get; set; } = 1;
	public int lapNumber { get; set; } = 0;
	public int nextCheckPoint { get; set; } = 0;
	public int onGroundID { get; set; } = 0;
	public bool isStunned { get; set; } = false;
	public float maxSpeed { get; set; }
	public PowerUp availablePowerUp { get; set; }

	private float lastDistance;
	private float inputVertical;
	private float inputHorizontal;
	private float rotationSpeed;
	private float acelerationForce;
	private float speed = 0;
	private bool RaceHasStarted = false;
	NavMeshPath playerPath;

	private void Awake()
	{
		runnerID = PlayerParameters.RunnerID;
		playerRigidbody = this.GetComponent<Rigidbody>();
		playerPath = new NavMeshPath();
		maxSpeed = PlayerParameters.MaxVelocity;
		rotationSpeed = PlayerParameters.RotationSpeed;
		acelerationForce = PlayerParameters.AcelerationForce;
		GameManager.Instance.runners.Add(this.GetComponent<IRunner>());
	}
	private void Start()
	{
		PathToNextCheckpoint();
		wrongDirectionWarning.SetActive(false);
		lastDistance = Direction();
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

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && (availablePowerUp != null))
		{
			UsePowerUp();
		}
		if (onGroundID == 0)
		{
			inputVertical = Input.GetAxis("Vertical");
			inputHorizontal = Input.GetAxis("Horizontal");
		}
		else if (onGroundID == 1)
		{
			inputVertical = -Input.GetAxis("Vertical");
			inputHorizontal = -Input.GetAxis("Horizontal");
		}
		else if (onGroundID == 2)
		{
			inputVertical = Input.GetAxis("Vertical");
			inputHorizontal = Input.GetAxis("Horizontal") / 2f;
		}

		RecalculateSpeed();
		Walk();

		if (Direction() > lastDistance)
		{
			lastDistance = Direction();
			wrongDirectionWarning.SetActive(true);
		}
		else if (Direction() < lastDistance)
		{
			lastDistance = Direction();
			wrongDirectionWarning.SetActive(false);
		}

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

	private void StartRace()
	{
		RaceHasStarted = true;
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
				speed = (maxSpeed / NavMesh.GetAreaCost(index));
			}
		}
		else
		{
			speed = 0;
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

	public float Direction()
	{
		PathToNextCheckpoint();
		var distance = Vector3.Distance(playerPath.corners[0], playerPath.corners[(playerPath.corners.Length - 1)]);
		return distance;
	}

	public bool WrongDirection()
	{
		PathToNextCheckpoint();
		var distance = Vector3.Distance(playerPath.corners[0], playerPath.corners[(playerPath.corners.Length - 1)]);
		Debug.Log(distance);
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

		if (nextCheckPoint < 6)
		{
			NavMesh.SamplePosition(GameManager.Instance.checkPoints[nextCheckPoint].position, out hit, 2f, NavMesh.AllAreas);
		}
		else
		{
			NavMesh.SamplePosition(GameManager.Instance.checkPoints[0].position, out hit, 2f, NavMesh.AllAreas);
		}
		NavMesh.CalculatePath(this.transform.position, hit.position, NavMesh.AllAreas, playerPath);

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