using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InfluenceManager : MonoBehaviour
{
    private static InfluenceManager _instance;
    public static InfluenceManager Instance { get { return _instance; } }
    public event Action InfluenceEmpty = delegate{};

    [SerializeField] float maxInfluence = 50.0f;
    [SerializeField] float influenceDecaySpeed = 0.1f;

    float currentInfluence;

    private void Awake()
    {
        currentInfluence = maxInfluence / 2f;

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    void Update()
    {
        currentInfluence -= Time.deltaTime * influenceDecaySpeed;
        if(currentInfluence <= 0.0f)
        {
            Debug.Log("Influence reached zero! Game over!");
            InfluenceEmpty();
        }
    }


    public void AddInfluence(float amount)
    {
        if(amount <= 0)
        {
            Debug.LogError("Can't add negative influence!");
            return;
        }

        currentInfluence += amount;
        currentInfluence = Mathf.Min(maxInfluence, currentInfluence);
    }

    public void ReduceInfluence(float amount)
    {
        if(amount <= 0)
        {
            Debug.LogError("Can't reduce negative influence!");
            return;
        }

        currentInfluence -= amount;
        if(currentInfluence <= 0.0f)
        {
            Debug.Log("Influence reached zero! Game over!");
            InfluenceEmpty();
        }
    }

    public float GetCurrentInfluence()
    {
        return currentInfluence;
    }

    public float GetMaxInfluence()
    {
        return maxInfluence;
    }

}
