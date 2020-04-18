using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColor : MonoBehaviour
{
    public float minSaturation;
    public float maxSaturation;

    public float minLightness;
    public float maxLightness;

    void Awake()
    {
        var renderer = GetComponent<Renderer>();
        var randomColor1 = Random.ColorHSV(0, 1, minSaturation, maxSaturation, minLightness, maxLightness);
        var randomColor2 = Random.ColorHSV(0, 1, minSaturation, maxSaturation, minLightness, maxLightness);
        renderer.material.SetColor("_Accent1Changed", randomColor1);
        renderer.material.SetColor("_Accent2Changed", randomColor2);
    }
}
