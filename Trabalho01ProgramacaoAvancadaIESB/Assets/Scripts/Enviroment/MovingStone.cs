using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStone : MonoBehaviour
{
    private Rigidbody stoneRigidbody;
    private Transform topPoint;
    private Transform bottomPoint;
    private float speed = 2f;
    private float timeOnTop = 3f;
    private float timeOnBottom = 3f;
    private bool startOnTop;

    private bool isOnTop;
    private bool isOnBottom;
    private float timer = 0;
    private float traveledDistance = 0;

    private void Awake()
    {
        stoneRigidbody = this.GetComponent<Rigidbody>();
        isOnTop = startOnTop;
        isOnBottom = !startOnTop;
    }

    private void Update()
    {
        if (isOnTop || isOnBottom)
        {
            timer += Time.deltaTime;
        }
        else
        {
            isOnBottom = (Vector3.Distance(this.transform.position, bottomPoint.position) < 0.1f);
            isOnTop = (Vector3.Distance(this.transform.position, topPoint.position) < 0.1f);
        }

        if (isOnBottom)
        {
            stoneRigidbody.useGravity = false;
        }

        if (isOnTop && timer > timeOnTop)
        {
            isOnTop = false;
            timer = 0;
            stoneRigidbody.useGravity = true;
        }

        if (isOnBottom && timer > timeOnBottom)
        {
            isOnBottom = false;
            timer = 0;
            moveToTop();
        }

    }

    private void moveToTop()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, topPoint.position, (timer * speed / traveledDistance));
    }
}