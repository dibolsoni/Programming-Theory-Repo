using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
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
    private LineRenderer lineRenderer;



    private void Start()
    {
        canoon = GetComponentInChildren<Canoon>();
        lastFire = Time.time - cooldown;
        lineRenderer = GetComponent<LineRenderer>();
        DrawSphereRange();
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
        if (isTowerSelected)
            lineRenderer.enabled = true;
        else
            lineRenderer.enabled = false;

    }

    private bool isTowerSelected
    {
        get
        {
            return BuilderManager.Instance.node != null && BuilderManager.Instance.node.tower == this;
        }
    }

    private void DrawSphereRange()
    {
        lineRenderer.positionCount = 360;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.useWorldSpace = false;
        for (int i = 0; i < 360; i++)
        {
            float rad = i * Mathf.Deg2Rad;
            lineRenderer.SetPosition(i, new Vector3(Mathf.Sin(rad) * range, 0, Mathf.Cos(rad) * range));
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


