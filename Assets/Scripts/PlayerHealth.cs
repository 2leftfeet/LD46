using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDestroyable
{
    public float health;
    public void Damage(float value)
    {
        health = health - value;
        if (health <= 0)
            Debug.Log("Player should be kaput");
    }
}
