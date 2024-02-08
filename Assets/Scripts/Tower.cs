using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Tower : MonoBehaviour
{
    [SerializeField]
    static GameObject target;
    public GameObject canoon;
    public float range = 10;
    private bool isTargetInRange = false;

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
            ResetCanoon();
        }
        else
        {
            CanoonLookAt(target.transform.position);
        }

    }

    private void ResetCanoon()
    {
        canoon.transform.rotation = transform.rotation;
    }

    private void CanoonLookAt(Vector3 pos)
    {
        canoon.transform.LookAt(pos);
    }


    public void FindNewTarget()
    {
        var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
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


