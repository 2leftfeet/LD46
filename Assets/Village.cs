using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Village : MonoBehaviour
{
    public float prosperity = 20;

    public TMPro.TMP_Text debugText;

    Spawner spawner;

    public void Awake()
    {
        spawner = GetComponent<Spawner>();
        spawner.avgWaitTime = 1/prosperity * 80;
        spawner.maxUnpossesedVillagerCount = (int)prosperity / 5;
        debugText.text = $"Prosperity = {prosperity}";
    }

    public void ModifyProsperity(float value)
    {
        prosperity += value;
        spawner.avgWaitTime = 1/prosperity * 80;
        spawner.maxUnpossesedVillagerCount = (int)prosperity / 5;
        debugText.text = $"Prosperity = {prosperity}";
    }
}
