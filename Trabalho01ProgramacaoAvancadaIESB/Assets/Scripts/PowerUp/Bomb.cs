using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;
    [SerializeField] private ParticleSystem explosionParticle;

    private void OnCollisionEnter(Collision other)
    {
        var runner = other.collider.GetComponent<IRunner>();
        if (runner != null)
        {
            explosionParticle.Play();

            Collider[ ] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider affected in colliders)
            {
                if (affected.GetComponent<IRunner>() != null)
                {
                    Rigidbody affectedRigidbody = affected.GetComponent<Rigidbody>();
                    if (affectedRigidbody != null)
                    {
                        affectedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                    }
                }
            }

            Destroy(this.gameObject);
        }
    }
}