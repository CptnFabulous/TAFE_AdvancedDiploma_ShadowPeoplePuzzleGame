using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int current = 10;
    public int max = 10;
    public int lives = 3;

    public UnityEvent onDamage;
    public UnityEvent onDie;

    public Collider[] Hitboxes { get; private set; }

    private void Awake()
    {
        Hitboxes = GetComponentsInChildren<Collider>(false);
    }

    public void Damage(int amount)
    {
        current -= amount;
        if (current <= 0)
        {
            Die();
        }
        else
        {
            onDamage.Invoke();
        }
    }

    float damageTimer = 0;
    public void DamageOverTime(float damagePerSecond)
    {
        damageTimer += Time.deltaTime;
        // Divides time to get a 
        if (damageTimer >= 1 / damagePerSecond)
        {
            damageTimer = 0;
            Damage(1);
        }
    }

    public void Die()
    {
        onDie.Invoke();
    }
}
