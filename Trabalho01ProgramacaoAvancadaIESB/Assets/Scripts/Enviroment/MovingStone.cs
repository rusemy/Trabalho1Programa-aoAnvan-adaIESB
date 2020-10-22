using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MovingStone : MonoBehaviour
{
    [SerializeField] private Rigidbody stoneRigidbody;
    [SerializeField] private Vector3[ ] pointsToMove;
    [SerializeField] private float stopTime = 3f;
    [SerializeField] private float speed = 0.5f;

    private float timer = 0;
    private float stopTimer = 0;
    private int nextPointIndex = 0;
    private Sequence tweenSequence;

    private void Awake()
    {
        stoneRigidbody = this.GetComponent<Rigidbody>();
        //transform.DOMove(pointsToMove[nextPointIndex], 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutBack).Play();

        tweenSequence = DOTween.Sequence();
        for (int i = 0; i < pointsToMove.Length; i++)
        {
            tweenSequence.Append(transform.DOMove(pointsToMove[i], 2f).SetEase(Ease.InOutQuad).From(pointsToMove[(i == 0) ? (pointsToMove.Length - 1) : (i - 1)]));
            if ((i == 0) || (i == 1) || (i == (pointsToMove.Length - 1)))
            {
                tweenSequence.AppendInterval(stopTime);
            }
        }
        tweenSequence.SetLoops(-1);
    }

    // private void Update()
    // {
    //     if (Vector3.Distance(this.transform.position, pointsToMove[nextPointIndex]) < 0.1f)
    //     {
    //         StartCoroutine(NextPoint());
    //     }
    //     else
    //     {
    //         //Debug.Log("actual:" + pointsToMove[actualPointIndex]);
    //         //Debug.Log("next:" + pointsToMove[nextPointIndex]);
    //         Vector3.Lerp(pointsToMove[actualPointIndex], pointsToMove[nextPointIndex], speed);
    //     }

    // }
}