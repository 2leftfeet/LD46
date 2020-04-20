using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSizeChanger : MonoBehaviour
{
    public float multiplier = 30f;
    Slider slider;

    RectTransform rectT;

    void Start()
    {
        slider = GetComponent<Slider>();
        rectT = GetComponent<RectTransform>();
    }
    void Update()
    {
        rectT.sizeDelta = new Vector2(slider.maxValue * multiplier, rectT.sizeDelta.y);
    }
}
