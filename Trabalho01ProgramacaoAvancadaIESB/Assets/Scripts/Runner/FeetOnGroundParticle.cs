using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetOnGroundParticle : MonoBehaviour
{
    //private ParticleSystem[ ] groundParticles = new ParticleSystem[5];
    private ParticleSystem particle;
    private ParticleSystemRenderer particleRenderer;
    private Material[ ] materials = new Material[5];
    private void OnTriggerEnter(Collider other)
    {
        var runner = this.GetComponent<IRunner>();
        if (runner != null)
        {
            //groundParticles[runner.onGroundID].Play();
            particleRenderer.material = materials[runner.onGroundID];
            particle.Play();
        }
    }
}