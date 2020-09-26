using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerParameters", menuName = "Parameters/PlayerParameters")]
public class PlayerParameters : ScriptableObject
{
    [SerializeField] private int runnerID;
    [SerializeField] private float maxVelocity = 10f;
    [SerializeField] private float acelerationForce = 100f;
    [SerializeField] private float rotationSpeed = 15f;

    public int RunnerID => runnerID;
    public float MaxVelocity => maxVelocity;
    public float AcelerationForce => acelerationForce;
    public float RotationSpeed => rotationSpeed;

}