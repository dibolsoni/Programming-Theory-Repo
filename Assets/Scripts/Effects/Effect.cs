using System;
using System.Collections;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    // ABSTRACTION
    public abstract Enemy target { get; protected set; }
    public ParticleSystem particlePrefab;
    private int _duration = 3;
    public int duration
    {
        get
        {
            return _duration;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Duration cannot be negative");
            }
            _duration = value;
        }
    }

    // ABSTRACTION
    abstract protected IEnumerator EffectRoutine();
    
    public void StartEffect()
    {
        particlePrefab.Play();
        StartCoroutine(EffectRoutine());
    }

}
