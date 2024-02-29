using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    protected GameObject target;
    // POLYMORPHISM
    public abstract float speed { get; } 
    // POLYMORPHISM
    public abstract int damage { get; }
    // POLYMORPHISM
    protected abstract void AfterHit(Enemy enemy);

    void Update()
    {
        if (target != null)
        {
            MoveToTarget();
        } else
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HitTarget();
        }
    }

    public void MoveToTarget()
    {
        Vector3 direction = target.transform.position - transform.position;
        transform.Translate(speed * Time.deltaTime * direction.normalized, Space.World);
    }

    public void SetNewTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    protected void HitTarget()
    {
        Destroy(gameObject);
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.Hit(damage);
        AfterHit(enemy);
    }

}
