using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay : MonoBehaviour
{
    [Header("Health")]
    public Health health;
    public Image healthMeter;
    public Text healthCounter;
    public Text lifeCounter;

    [Header("Interaction")]
    public RectTransform interactWindow;
    public Text interactableName;
    public Text prompt;

    public void Refresh()
    {
        RefreshHealthMeter();
        RefreshInteractionWindow(null);
        
    }

    public void RefreshHealthMeter()
    {
        healthMeter.fillAmount = health.current / health.max;
        healthCounter.text = health.current.ToString();
        lifeCounter.text = health.lives.ToString();
    }
    
    public void RefreshInteractionWindow(Interactable i)
    {
        interactWindow.gameObject.SetActive(i != null);
        if (i != null)
        {
            interactableName.text = i.name;
            if (i.IsInteractable == true)
            {
                prompt.text = i.prompt;
            }
            else
            {
                prompt.text = i.deniedMessage;
            }
            
        }
    }
    
    
    
}
