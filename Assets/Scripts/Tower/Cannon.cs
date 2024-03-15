using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Cannon : MonoBehaviour
{
    public GameObject spawnProjectile;
    public GameObject projectile;
    public float turnSpeed = 50f;
    public AudioSource audioSource;


    public void Look(GameObject target)
    {
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

    public void Fire(Enemy target)
    {
        audioSource.Play();
        var p = Instantiate(projectile, spawnProjectile.transform.position, transform.rotation).GetComponent<BaseProjectile>();
        p.SetNewTarget(target);
    }
}
