using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    protected Enemy target;
    // ABSTRACTION
    public abstract float speed { get; }
    // ABSTRACTION
    public abstract int damage { get; }
    // ABSRACTION
    protected abstract void AfterHit(Enemy enemy);

    private void FixedUpdate()
    {
        if (target != null)
        {
            MoveToTarget();
        }
        else
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
        Vector3 targetOffset = new Vector3(0, 2.5f, 0);
        Vector3 targetPosition = target.transform.position + targetOffset;
        Vector3 direction = targetPosition - transform.position;
        transform.Translate(speed * Time.deltaTime * direction.normalized, Space.World);
    }

    public void SetNewTarget(Enemy newTarget)
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
