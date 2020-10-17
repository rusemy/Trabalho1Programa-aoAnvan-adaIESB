using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStone : MonoBehaviour
{
    [SerializeField] private Rigidbody stoneRigidbody;
    [SerializeField] private Vector3[ ] pointsToMove;
    [SerializeField] private float stopTime = 3f;
    [SerializeField] private float speed = 0.5f;

    private float timer = 0;
    private float stopTimer = 0;
    private int actualPointIndex = 0;
    private int nextPointIndex = 1;

    private void Awake()
    {
        stoneRigidbody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Vector3.Distance(this.transform.position, pointsToMove[nextPointIndex]) < 0.1f)
        {
            StartCoroutine(NextPoint());
        }
        else
        {
            Debug.Log("actual:" + pointsToMove[actualPointIndex]);
            Debug.Log("next:" + pointsToMove[nextPointIndex]);
            Vector3.Lerp(pointsToMove[actualPointIndex], pointsToMove[nextPointIndex], speed);
        }

    }

    private IEnumerator NextPoint()
    {
        yield return new WaitForSeconds(stopTime);
        actualPointIndex = nextPointIndex;
        nextPointIndex++;
        if (nextPointIndex >= pointsToMove.Length)
        {
            nextPointIndex = 0;
        }
    }
}