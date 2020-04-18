﻿using System;
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
    public bool CanSacrifice { set; get; }

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
            var list = possessedVillagers.possessedVillagers;
            if (Input.GetKeyDown(sacrificeButton) && list.Count != 0){
                list[0].GoSacrificeSelf(this, collider.transform);
                list.RemoveAt(0);
            }
        }
    }

    public void Sacrifice(VillagerAI target)
    {
        target.GetComponent<SacrificeParticles>().SpawnSacrificeParticles();
        Destroy(target.gameObject);

        OnSacrifice?.Invoke();
    }
}
