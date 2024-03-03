using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// INHERITANCE
public class ColdProjectile : BaseProjectile
{
    // POLYMORPHISM
    public override int damage => 25;
    // POLYMORPHISM
    public override float speed => 15f;

    // POLYMORPHISM
    protected override void AfterHit(Enemy enemy)
    {
        enemy.Slow();
    }
}
