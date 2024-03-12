using System;
using System.Collections;
using UnityEngine;

public class SlowEffect : Effect
{
    override public Enemy target { get; protected set; }

    private float power;

    // POLYMORPHISM
    protected override IEnumerator EffectRoutine()
    {
        while (duration > 0)
        {
            particlePrefab.Play();
            if (target != null)
                target.speed -= power;
            duration--;
            yield return new WaitForSeconds(1);
        }
        target.speed = target.originalSpeed;
        particlePrefab.Stop();
    }

    public SlowEffect StartSlow(Enemy targetToSlow, float slowPower)
    {
        duration = 3;
        target = targetToSlow;
        power = slowPower;
        StartEffect();
        return this;
    }
}
