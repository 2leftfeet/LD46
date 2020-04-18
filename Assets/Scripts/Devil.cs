using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour
{
    [SerializeField] int maxHealth;

    [SerializeField] SliderBarScript healthBar;
    int currentHealth;

    void Start() {
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
    }

}
