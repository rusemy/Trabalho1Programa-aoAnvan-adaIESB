using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPowerUp : PowerUp
{
    [SerializeField] private float boostDuration;
    [SerializeField] private float boostAmount;
    public override IEnumerator Effect(IRunner runner, Transform parent)
    {
        var particleSystem = Instantiate(effectParticle, parent);
        runner.maxSpeed *= boostAmount;

        yield return new WaitForSeconds(boostDuration);

        runner.maxSpeed /= boostAmount;
        Destroy(particleSystem);
    }
}