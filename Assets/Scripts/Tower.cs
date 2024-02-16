using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private Canoon canoon;
    private float lastFire = 0;
    private bool isTargetInRange = false;

    public GameObject target { get; private set; }
    public float range = 10;
    public float cooldown = 0.5f;


    private void Start()
    {
        canoon = GetComponentInChildren<Canoon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if (target.IsDestroyed())
            {
                target = null;
            }

            if (Vector3.Distance(target.transform.position, transform.position) > range)
            {
                isTargetInRange = false;
            }
            else
            {
                isTargetInRange = true;
            }
        }
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
            canoon.Look(target);
        }

    }

    private void Fire()
    {
        if (Time.time > lastFire + cooldown)
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
                target = enemy.gameObject;
                break;
            }
        }
    }
}


