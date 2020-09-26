using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderPowerUp : PowerUp
{
    public float stunDuration;
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