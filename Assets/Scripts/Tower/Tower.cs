using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Tower : MonoBehaviour
{
    private Canoon canoon;
    public int cost;

    private bool isTargetInRange
    {
        get
        {
            return target is not null && Vector3.Distance(target.transform.position, transform.position) <= range;
        }
    }
    private Enemy target;
    private float lastFire;
    public float range = 10;
    public float cooldown = 1f;


    private void Start()
    {
        canoon = GetComponentInChildren<Canoon>();
        lastFire = Time.time - cooldown;
    }

    private void LateUpdate()
    {
        if (target == null || !isTargetInRange)
        {
            FindNewTarget();
            canoon.Reset();
        }
        else
        {
            Fire();
            canoon.Look(target.gameObject);
        }

    }

    private void Fire()
    {
        float elapsedTime = Time.time - lastFire;
        if (elapsedTime > cooldown)
        {
            lastFire = Time.time;
            canoon.Fire(target);
        }
    }



    public void FindNewTarget()
    {
        var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.InstanceID);
        foreach (var enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= range)
            {
                target = enemy;
                break;
            }
        }
    }
}


