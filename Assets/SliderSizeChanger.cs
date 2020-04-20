using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSizeChanger : MonoBehaviour
{
    public float multiplier = 30f;
    public Slider slider;

    public RectTransform rectT;

    void Start()
    {
        rectT = slider.GetComponent<RectTransform>();
    }
    void Update()
    {
        rectT.sizeDelta = new Vector2(slider.maxValue * multiplier, rectT.sizeDelta.y);
    }
}
