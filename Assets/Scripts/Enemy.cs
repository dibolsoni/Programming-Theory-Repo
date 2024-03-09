using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float originalSpeed;
    public float _speed = 10f;
    private float minSpeed = 5f;

    // ENCAPSULATION
    public float speed
    {
        get { return _speed; }
        set { _speed = Math.Max(minSpeed, value); }
    }
    private Transform target;
    public Vector3 enemyTargetablePoint { get { return target.position + new Vector3(0, 2.5f, 0); } }
    private int waypointIndex = 0;
    public int health = 100;
    public int goldValue = 10;
    private float turnSpeed = 10f;
    private Color originalColor;
    private GameObject ghoul;

    // Start is called before the first frame update
    void Start()
    {
        ghoul = transform.GetChild(0).gameObject;
        originalSpeed = speed;
        originalColor = GetComponent<Renderer>().material.color;
        target = Waypoints.points[waypointIndex];
        IncrementHealth(GameManager.Instance.waveNumber);
    }

    void IncrementHealth(int waveNumber)
    {
        health += waveNumber * 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
        MoveToWayPoint();
        LookToWaypoint();
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        if (target)
        {
            if (speed > minSpeed)
                ghoul.GetComponent<Animation>().Play("Run");
            else if (speed == minSpeed)
                ghoul.GetComponent<Animation>().Play("Walk");
            else
                ghoul.GetComponent<Animation>().Play("Idle");
        }
    }

    public void MoveToWayPoint()
    {

        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    public void LookToWaypoint()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void GetNextWaypoint()
    {
        if ((waypointIndex + 1) >= Waypoints.points.Length)
        {
            GameManager.Instance.removeLife();
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
            GameManager.Instance.addGold(goldValue);
        }
    }

    IEnumerator SlowRoutine(float power = 5f)
    {
        int numberOfTicks = 3;
        speed -= power;
        MixColor(Color.blue);
        for (int i = 0; i < numberOfTicks; i++)
        {
            yield return new WaitForSeconds(1);
        }
        speed = originalSpeed;
        GetComponent<Renderer>().material.color = originalColor;
    }

    public void Slow(float power = 5f)
    {
        StartCoroutine(SlowRoutine(power));
    }

    IEnumerator BurnRoutine(int damage)
    {
        int numberOfTicks = 3;
        MixColor(Color.yellow);
        for (int i = 0; i < numberOfTicks; i++)
        {
            Hit(damage / numberOfTicks);
            yield return new WaitForSeconds(1);
        }
        GetComponent<Renderer>().material.color = originalColor;
    }

    public void Burn(int damage = 5)
    {
        StartCoroutine(BurnRoutine(damage));
    }

    public void MixColor(Color color)
    {
        var _color = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.Lerp(_color, color, 0.5f);
    }
}
