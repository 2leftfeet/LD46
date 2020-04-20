using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Just a flag component atm
/// </summary>
public class DemonGod : MonoBehaviour, IDestroyable
{
    public static DemonGod instance;
    [SerializeField] float flashTime = 0.1f;

    float flashTimer = 0.0f;
    bool isFlashed = false;
    Material material;

    public void Damage(float value)
    {
        isFlashed = true;
        material.SetFloat("FullWhite", 1.0f);
        
        flashTimer = flashTime;
        InfluenceManager.Instance.ReduceInfluence(value);
    }

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }

    
    public void Update(){
        if(isFlashed)
        {
            if(flashTimer <= 0.0f)
            {
                isFlashed = false;
                material.SetFloat("FullWhite", 0.0f);
            }
            else
            {
                flashTimer -= Time.deltaTime;
            }
        }
    }
}
