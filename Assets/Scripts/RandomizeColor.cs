using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColor : MonoBehaviour
{
    public Vector3 skinMinHSV;
    public Vector3 skinMaxHSV;

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

        skinMinHSV /= 255;
        skinMaxHSV /= 255;

        var randomSkin = Random.ColorHSV(skinMinHSV.x, skinMaxHSV.x, skinMinHSV.y, skinMaxHSV.y, skinMinHSV.z, skinMaxHSV.z);
        renderer.material.SetColor("_SkinChanged", randomSkin);
    }
}
