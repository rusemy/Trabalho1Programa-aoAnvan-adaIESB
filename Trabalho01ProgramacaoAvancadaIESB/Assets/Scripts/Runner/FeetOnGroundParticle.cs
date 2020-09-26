using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetOnGroundParticle : MonoBehaviour
{
    private ParticleSystem[ ] groundParticles = new ParticleSystem[5];
    private void OnTriggerEnter(Collider other)
    {
        var ground = other.GetComponent<Ground>();
        if (ground != null)
        {
            groundParticles[ground.groundID].Play();
        }
    }
}