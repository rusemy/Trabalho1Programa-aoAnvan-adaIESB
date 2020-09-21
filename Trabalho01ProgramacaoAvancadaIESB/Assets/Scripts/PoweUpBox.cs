using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweUpBox : MonoBehaviour
{
    private ParticleSystem onEnableParticle;
    private ParticleSystem onDisableParticle;
    private ParticleSystem readyToPickParticle;
    private GameObject box;
    private float timeToRespawn;

    private void OnEnable()
    {
        onEnableParticle.Play();
    }

    private void OnDisable()
    {
        readyToPickParticle.Stop();
        onEnableParticle.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DespawnAndRespawnBox());
    }

    IEnumerator DespawnAndRespawnBox()
    {
        box.SetActive(false);
        yield return new WaitForSeconds(timeToRespawn);
        box.SetActive(true);
    }
}