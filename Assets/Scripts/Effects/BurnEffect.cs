using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
public class BurnEffect : Effect
{
    override public Enemy target { get; protected set; }

    private int damage;

    // POKYMORPHISM
    override protected IEnumerator EffectRoutine()
    {
        while (duration > 0)
        {
            particlePrefab.Play();
            if (target != null)
                target.Hit(damage);
            duration--;
            yield return new WaitForSeconds(1);
        }
    }


    public BurnEffect StartBurn(Enemy targetToBurn, int burnDamage)
    {
        duration = 3;
        target = targetToBurn;
        damage = burnDamage;
        StartEffect();
        return this;
    }

}