using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected GameManager gameManager;
    public float speed = 10f;
    private Transform target;
    private int waypointIndex = 0;
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.points[waypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

        transform.Rotate(Vector3.forward * Time.deltaTime * speed);
    }

    private void GetNextWaypoint()
    {
        if ((waypointIndex + 1)  >= Waypoints.points.Length) {
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    public void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
