using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetOnGroundParticle : MonoBehaviour
{
    //private ParticleSystem[ ] groundParticles = new ParticleSystem[5];
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private ParticleSystemRenderer particleRenderer;
    [SerializeField] private Material[ ] materials = new Material[5];
    private void OnTriggerEnter(Collider other)
    {

        var runner = this.GetComponentInParent<IRunner>();
        if (runner != null)
        {
            //groundParticles[runner.onGroundID].Play();
            particleRenderer.material = materials[runner.onGroundID];
            particle.Play();

        }
    }
}