using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Thunder", menuName = "PowerUps/Thunder")]

public class ThunderPowerUp : PowerUp
{
    [SerializeField] private float stunDuration;
    public override IEnumerator Effect(IRunner runner, Transform parent)
    {
        var particleSystem = Instantiate(effectParticle, parent);
        foreach (var runnerToCheck in GameManager.Instance.runners)
        {
            if (runnerToCheck.runnerID != runner.runnerID)
            {
                runnerToCheck.isStunned = true;
            }
        }

        yield return new WaitForSeconds(stunDuration);

        foreach (var runnerToCheck in GameManager.Instance.runners)
        {
            if (runnerToCheck.runnerID != runner.runnerID)
            {
                runnerToCheck.isStunned = false;
            }
        }
        Destroy(particleSystem);
    }
}