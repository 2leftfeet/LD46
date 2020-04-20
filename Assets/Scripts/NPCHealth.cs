using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour, IDestroyable
{
    [SerializeField] float maxHealth;
    [SerializeField] float flashTime = 0.2f;

    float currentHealth;
    float flashTimer = 0.0f;
    bool isFlashed = false;
    Material material;
    LootDrop loot;

    public void Update()
    {
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

    public void Damage(float value)
    {
        currentHealth -= value;
        isFlashed = true;
        material.SetFloat("FullWhite", 1.0f);
        
        flashTimer = flashTime;
        if(currentHealth <= 0){
            Destroy(this.gameObject);
            loot.DropLoot();
        }
    }

    void Awake()
    {
        currentHealth = maxHealth;
        material = GetComponent<Renderer>().material;
        loot = GetComponent<LootDrop>();
    }
}
