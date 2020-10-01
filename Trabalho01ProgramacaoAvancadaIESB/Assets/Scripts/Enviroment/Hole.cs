using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hole : MonoBehaviour
{
    IRunner runner;
    Collision collided;
    private void OnCollisionEnter(Collision other)
    {
        collided = other;
        runner = other.collider.GetComponent<IRunner>();
        if (runner != null)
        {
            StartCoroutine(ReturnToRace());
        }
    }

    IEnumerator ReturnToRace()
    {
        runner.isStunned = true;
        runner.availablePowerUp = null;

        yield return new WaitForSeconds(2f);

        NavMeshHit hit;
        NavMesh.SamplePosition(collided.GetContact(0).point, out hit, 15f, NavMesh.AllAreas);
        collided.transform.position = hit.position;
        runner.isStunned = false;
    }
}