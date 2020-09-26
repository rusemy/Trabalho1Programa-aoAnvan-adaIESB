using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPowerUp : PowerUp
{
    public float boostDuration;
    public float boostAmount;
    public override IEnumerator Effect(IRunner runner, Transform parent)
    {
        var particleSystem = Instantiate(effectParticle, parent);
        runner.maxSpeed *= boostAmount;

        yield return new WaitForSeconds(boostDuration);

        runner.maxSpeed /= boostAmount;
        Destroy(particleSystem);
    }
}