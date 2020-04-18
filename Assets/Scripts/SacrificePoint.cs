using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificePoint : MonoBehaviour
{
    public static event Action OnSacrifice;
    private PossessedVillagers possessedVillagers;

    public float radius = 2.5f;
    public KeyCode sacrificeButton;

    /// <summary>
    /// Could be used for key indication
    /// </summary>
    public bool IsSacrificeInRange { set; get; }

    void Awake()
    {
        possessedVillagers = GetComponent<PossessedVillagers>();
    }

    // Update is called once per frame
    void Update()
    {
        var collider = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Sacrifice"));
        if (collider)
        {
            if(Input.GetKeyDown(sacrificeButton)){
                Sacrifice(possessedVillagers.possessedVillagers[0]);
            }
        }
    }

    void Sacrifice(VillagerAI target)
    {
        possessedVillagers.possessedVillagers.Remove(target);
        target.GetComponent<SacrificeParticles>().SpawnSacrificeParticles();
        Destroy(target.gameObject);

        OnSacrifice?.Invoke();
    }
}
