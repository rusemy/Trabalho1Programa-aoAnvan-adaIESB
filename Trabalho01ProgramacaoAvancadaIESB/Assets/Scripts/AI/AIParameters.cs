using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIParameters", menuName = "Parameters/AIParameters")]
public class AIParameters : ScriptableObject
{
    [SerializeField] private int runnerID;
    [SerializeField] private float maxVelocity = 10f;

    public int RunnerID => runnerID;
    public float MaxVelocity => maxVelocity;

}