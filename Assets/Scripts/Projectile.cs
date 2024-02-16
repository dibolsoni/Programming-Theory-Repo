using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    public float speed = 20f;
    public int damage = 50;

    void Update()
    {
        if (target != null)
        {
            MoveToTarget();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HitTarget();
            Destroy(gameObject);
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

    private void HitTarget()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.Hit(damage);
    }
}
