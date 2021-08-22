using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay : MonoBehaviour
{

    public Health health;
    public Image healthMeter;
    public Text healthCounter;
    public Text lifeCounter;



    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthMeter();
    }

    public void UpdateHealthMeter()
    {
        healthCounter.text = health.current.ToString();
        healthMeter.fillAmount = health.current / health.max;
        lifeCounter.text = health.lives.ToString();
    }
    
    
    
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
