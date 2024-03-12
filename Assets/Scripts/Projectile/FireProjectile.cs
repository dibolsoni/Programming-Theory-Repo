using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class FireProjectile : BaseProjectile
{
    // POLYMORPHISM
    public override int damage => 50;
    // POLYMORPHISM
    public override float speed => 20f;
    // POLYMORPHISM
    protected override void AfterHit(Enemy enemy)
    {
        enemy.Burn(25);
    }
}
