using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour{
    public Slider slider;

    public void SetMaxHealth(float val)
    {
        slider.maxValue = val;
        slider.value = val;
    }
    
    public void SetHealth(float val)
    {
        slider.value = val;
    }
}
