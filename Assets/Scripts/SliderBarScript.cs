using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBarScript : MonoBehaviour
{
    [SerializeField] Slider slider;

    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    [SerializeField] bool isDecreasing = false;

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
        slider.maxValue = InfluenceManager.Instance.maxInfluence;
        slider.value = InfluenceManager.Instance.currentInfluence;
    }

    private void Update() 
    {
        slider.value = InfluenceManager.Instance.currentInfluence;
        fill.color = gradient.Evaluate(slider.normalizedValue);
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