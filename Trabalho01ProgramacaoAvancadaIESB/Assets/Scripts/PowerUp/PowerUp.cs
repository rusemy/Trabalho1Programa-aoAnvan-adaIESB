using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : ScriptableObject
{
    public new string name = "No Power Up";
    public float duration = 0;
    public Sprite icon;
    public ParticleSystem effectParticle;

    public abstract IEnumerator Effect(IRunner runner, Transform parent);
}