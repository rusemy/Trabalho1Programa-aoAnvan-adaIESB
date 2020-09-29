using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIParameters", menuName = "Parameters/AIParameters")]
public class AIParameters : ScriptableObject
{
    [SerializeField] private float maxVelocity = 10f;

    public float MaxVelocity => maxVelocity;

}