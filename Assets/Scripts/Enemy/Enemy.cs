using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    public float originalSpeed;
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
    public int maxHealth = 100;
    public int health;
    public int goldValue = 10;
    private float turnSpeed = 10f;

    private GameObject ghoul;
    private BurnEffect burnEffect;
    private SlowEffect slowEffect;
    public AudioClip endPathSound;
    public AudioClip spawnSound;
    public AudioClip dieSound;
    public List<Effect> effects;
    private AudioSource audioSource;
   


    private void Awake()
    {
        IncrementHealth();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(spawnSound);
        ghoul = transform.GetChild(0).gameObject;
        originalSpeed = speed;
        target = Waypoints.points[waypointIndex];
        effects = new List<Effect>();
        burnEffect = GetComponent<BurnEffect>();
        slowEffect = GetComponent<SlowEffect>();
        health = maxHealth;
    }

    void IncrementHealth()
    {
        maxHealth += SpawnManager.Instance.WaveNumber * 10;
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
            audioSource.PlayOneShot(endPathSound);
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
            if (audioSource.isActiveAndEnabled) 
                audioSource.PlayOneShot(dieSound);
            Destroy(gameObject);
            GameManager.Instance.addGold(goldValue);
        }
    }

    public void Slow(float power = 5f)
    {
        if (effects.Any(e => e is SlowEffect))
        {
            var slow = (SlowEffect)effects.First(e => e is SlowEffect);
            slow.duration = 3;
        }
        else
        {
            effects.Add(slowEffect.StartSlow(this, power));
        }
    }


    public void Burn(int damage = 5)
    {
        if (effects.Any(e => e is BurnEffect))
        {
            var burn = (BurnEffect)effects.First(e => e is BurnEffect);
            burn.duration = 3;
        }
        else
        {
            effects.Add(burnEffect.StartBurn(this, damage));
        }
    }
}
