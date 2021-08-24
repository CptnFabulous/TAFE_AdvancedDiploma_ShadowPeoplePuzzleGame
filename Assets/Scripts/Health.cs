using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public Player player { get; set; }
    
    public int current = 10;
    public int max = 10;
    public int lives = 3;

    public UnityEvent onDamage;
    public UnityEvent onDie;

    public Collider[] Hitboxes { get; private set; }

    private void Awake()
    {
        player = GetComponent<Player>();
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
        

        while(damageTimer >= 1 / damagePerSecond)
        {
            Damage(1);
            damageTimer -= 1 / damagePerSecond;
        }
        
        /*
        if (damageTimer >= 1 / damagePerSecond)
        {
            damageTimer = 0;
            current -= 1;
        }
        */
    }

    public void Die()
    {
        onDie.Invoke();

        if (lives > 0)
        {
            lives -= 1;
            SpawnPoint.Current.Respawn(player);
        }
        else
        {
            LevelData.Current.FailLevel();
        }
    }

}
