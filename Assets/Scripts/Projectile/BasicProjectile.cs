using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class BasicProjectile : BaseProjectile
{
    // ABSTRACTION
    public override int damage => 50;
    // ABSTRACTION
    public override float speed => 20f;

    // ABSTRACTION
    protected override void AfterHit(Enemy enemy)
    {
        // Do nothing
    }
}
