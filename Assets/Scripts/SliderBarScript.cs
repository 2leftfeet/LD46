using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum SliderType
{
    Influence,
    VillageProsperity,
    VillageSpawn
}
public class SliderBarScript : MonoBehaviour
{
    [SerializeField] SliderType type;
    [SerializeField] Slider slider;

    [SerializeField] Gradient gradient;
    [SerializeField] Image fillBar;
    [SerializeField] Image fillIcon;

    [SerializeField] bool isDecreasing = false;
    [SerializeField] Village village;
    [SerializeField] Spawner spawner;

    public void SetValue(int value)
    {
        slider.value = value;
    }

    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    private void Start() {
        switch(type)
        {
            case SliderType.Influence:
                slider.maxValue = InfluenceManager.Instance.maxInfluence;
                slider.value = InfluenceManager.Instance.currentInfluence;
                break;
            case SliderType.VillageProsperity:
                slider.maxValue = village.prosperity;
                slider.value = village.prosperity;
                break;
            case SliderType.VillageSpawn:
                slider.maxValue = spawner.avgWaitTime;
                slider.value = spawner.avgWaitTime - spawner.waitTimer;
                break;
        }
        
    }

    private void Update() 
    {
        switch(type)
        {
            case SliderType.Influence:
                slider.value = InfluenceManager.Instance.currentInfluence;
                break;
            case SliderType.VillageProsperity:
                slider.maxValue = village.prosperity;
                slider.value = village.prosperity;
                break;
            case SliderType.VillageSpawn:
                slider.maxValue = spawner.avgWaitTime;
                slider.value = spawner.avgWaitTime - spawner.waitTimer;
                break;
        }
        
        fillBar.color = gradient.Evaluate(slider.normalizedValue);
        fillIcon.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void AddSliderValue(int amount)
    {
        slider.value += amount;
    }

    public void DecreaseSliderValue( int amount)
    {
        slider.value -= amount;
    }
}