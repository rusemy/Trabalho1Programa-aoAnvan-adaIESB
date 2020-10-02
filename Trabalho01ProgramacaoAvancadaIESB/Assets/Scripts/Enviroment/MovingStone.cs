using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStone : MonoBehaviour
{
    [SerializeField] private Transform[ ] pointsToMove;
    [SerializeField] private float stopTime = 3f;
    [SerializeField] private float speed = 2f;

    private float timer = 0;
    private float stopTimer = 0;
    private float traveledDistance = 0;
    private int actualPointIndex;
    private int nextPointIndex;

    private void Awake()
    {
        traveledDistance = Vector3.Distance(pointsToMove[actualPointIndex].position, pointsToMove[nextPointIndex].position);
    }

    private void Update()
    {
        stopTimer += Time.deltaTime;
        if (traveledDistance > 1)
        {
            NextPoint();
        }

        if (stopTimer > stopTime)
        {
            Move();
        }

    }

    private void Move()
    {
        timer += Time.deltaTime;

        this.transform.position = Vector3.Lerp(pointsToMove[actualPointIndex].position, pointsToMove[nextPointIndex].position, (timer * speed / traveledDistance));
    }

    private void NextPoint()
    {
        timer = 0;
        stopTime = 0;
        actualPointIndex = nextPointIndex;
        nextPointIndex++;
        if (nextPointIndex >= pointsToMove.Length)
        {
            nextPointIndex = 0;
        }
        traveledDistance = Vector3.Distance(pointsToMove[actualPointIndex].position, pointsToMove[nextPointIndex].position);
    }
}