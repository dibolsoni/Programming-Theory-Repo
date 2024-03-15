using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// INHERITANCE
public class ColdProjectile : BaseProjectile
{
    // ABSTRACTION
    public override int damage => 25;
    // ABSTRACTION
    public override float speed => 15f;
    // ABSTRACTION
    protected override void AfterHit(Enemy enemy)
    {
        enemy.Slow();
    }
}
