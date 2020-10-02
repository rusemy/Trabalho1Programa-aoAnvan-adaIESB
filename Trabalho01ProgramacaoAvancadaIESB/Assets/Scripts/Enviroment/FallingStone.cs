using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStone : MonoBehaviour
{
    [SerializeField] private Rigidbody stoneRigidbody;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform bottomPoint;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float timeOnTop = 3f;
    [SerializeField] private float timeOnBottom = 3f;
    [SerializeField] private bool startOnTop;

    private bool isOnTop;
    private bool isOnBottom;
    private float timer = 0;
    private float traveledDistance = 0;

    private void Awake()
    {
        stoneRigidbody = this.GetComponent<Rigidbody>();
        isOnTop = startOnTop;
        isOnBottom = !startOnTop;
        traveledDistance = Vector3.Distance(topPoint.position, bottomPoint.position);
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
            MoveToTop();
        }

    }

    private void MoveToTop()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, topPoint.position, (timer * speed / traveledDistance));
    }
}