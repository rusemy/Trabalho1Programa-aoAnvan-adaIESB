using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweUpBox : MonoBehaviour
{
    [SerializeField] private ParticleSystem onEnableParticle;
    [SerializeField] private ParticleSystem onDisableParticle;
    [SerializeField] private ParticleSystem readyToPickParticle;
    [SerializeField] private GameObject box;
    [SerializeField] private float timeToRespawn;
    [SerializeField] private PowerUp[ ] powerUps;
    private PowerUp powerUpToPick;

    private void OnEnable()
    {
        var powerUpSelected = Random.Range(0, powerUps.Length);
        powerUpToPick = powerUps[powerUpSelected];
        onEnableParticle.Play();
    }

    private void OnDisable()
    {
        powerUpToPick = null;
        readyToPickParticle.Stop();
        onEnableParticle.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        var runner = other.GetComponent<IRunner>();
        if (runner.availablePowerUp == null)
        {
            runner.availablePowerUp = powerUpToPick;
            if (other.GetComponent<AIController>() != null)
            {
                other.GetComponent<AIController>().timeToUsePowerUp = Random.Range(0.1f, (powerUpToPick.duration * 0.8f));
            }
            StartCoroutine(DespawnAndRespawnBox());
        }
    }

    IEnumerator DespawnAndRespawnBox()
    {
        box.SetActive(false);
        yield return new WaitForSeconds(timeToRespawn);
        box.SetActive(true);
    }
}