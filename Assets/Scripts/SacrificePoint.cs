using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificePoint : MonoBehaviour
{
    public static event Action OnSacrifice;
    private PossessedVillagers possessedVillagers;

    public float radius = 1.5f;
    public float influencePerSacrifice = 3.0f;
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
        var list = possessedVillagers.possessedVillagers;
        if (collider && list.Count > 0)
        {          
            if (Input.GetKeyDown(sacrificeButton)){
                list[list.Count - 1].GoSacrificeSelf(this, collider.transform);
                list.RemoveAt(list.Count -1 );
            }
        }
    }

    public void Sacrifice(VillagerAI target)
    {
        target.GetComponent<SacrificeParticles>().SpawnSacrificeParticles();
        Destroy(target.gameObject);

        InfluenceManager.Instance.AddInfluence(influencePerSacrifice);
        SoundManager.Instance.CreatePlayAndDestroy(SoundManager.Instance.sacrificeSounds[UnityEngine.Random.Range(0,2)], 0.1f);

        OnSacrifice?.Invoke();
    }
}
