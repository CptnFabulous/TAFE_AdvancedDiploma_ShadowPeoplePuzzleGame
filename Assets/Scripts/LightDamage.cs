using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightDamage : MonoBehaviour
{
    Health health;
    public float damageMultiplier = 1;

    private void Awake()
    {
        health = GetComponent<Health>();
    }
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }
    */
    // Update is called once per frame
    void Update()
    {
        float totalAmountOfLight = 0;
        LightSource[] lights = FindObjectsOfType<LightSource>();
        for (int i = 0; i < lights.Length; i++)
        {
            totalAmountOfLight += lights[i].HowMuchLightIsHittingThing(health.Hitboxes);
        }

        if (totalAmountOfLight > 0)
        {
            health.DamageOverTime(totalAmountOfLight * damageMultiplier);
        }
    }
}
