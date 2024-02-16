using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Canoon : MonoBehaviour
{
    public GameObject projectile;
    public GameObject spawnPosition;

    public float turnSpeed = 50f;



    public void Look(GameObject target) {
        transform.rotation = LerpRotation(target.transform.position);
    }

    private Quaternion LerpRotation(Vector3 toPosition)
    {
        Vector3 direction = toPosition - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        return Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void Reset()
    {
        transform.rotation = LerpRotation(Quaternion.identity.eulerAngles);
    }

    public void Fire(GameObject target)
    {
        Projectile p = Instantiate(projectile, spawnPosition.transform.position, transform.rotation).GetComponent<Projectile>();
        p.SetNewTarget(target);
    }
}
