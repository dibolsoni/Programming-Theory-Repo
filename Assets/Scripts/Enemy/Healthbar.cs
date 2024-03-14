using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider healthbar;
    public Slider easeHealthbar;
    public Enemy enemy;
    private float lerpSpeed = 0.05f;

    private void Start()
    {
        healthbar.maxValue = (float)enemy.maxHealth;
        easeHealthbar.maxValue = (float)enemy.maxHealth;
        healthbar.value = enemy.maxHealth;
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateEaseHealthbar();
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }

    private void UpdateHealthBar()
    {
        if (healthbar.value != enemy.health)
        {
            healthbar.value = (float) enemy.health;
        }
    }

    private void UpdateEaseHealthbar()
    {
        if (healthbar.value != easeHealthbar.value)
        {
            easeHealthbar.value = Mathf.Lerp(easeHealthbar.value, healthbar.value, lerpSpeed);
        }
    }

}
