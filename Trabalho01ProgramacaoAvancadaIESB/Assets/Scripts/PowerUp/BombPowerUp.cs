using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPowerUp : PowerUp
{
    [SerializeField] private float timeToPlaceBomb;
    [SerializeField] private float distanceToPlaceBomb;
    [SerializeField] private GameObject bombPrefab;

    public override IEnumerator Effect(IRunner runner, Transform parent)
    {
        var particleSystem = Instantiate(effectParticle, parent);

        yield return new WaitForSeconds(timeToPlaceBomb);

        Instantiate(bombPrefab, parent.position, Quaternion.identity);
        Destroy(particleSystem);
    }
}