using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    public float speed = 20f;

    void LateUpdate()
    {
        if (target != null)
        {
            MoveToTarget(target);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            Destroy(gameObject);
    }

    public void MoveToTarget(GameObject newTarget)
    {
            Vector3 direction = target.transform.position - transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * speed);
    }

    public void SetNewTarget(GameObject newTarget)
    {
        target = newTarget;
    }
}
