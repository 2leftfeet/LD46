using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem sacrificeParticles;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SpawnSacrificeParticles()
    {
        var spawnedParticles = Instantiate(sacrificeParticles, transform.position, Quaternion.identity);

        var shape = spawnedParticles.shape;
        shape.sprite = spriteRenderer.sprite;

        var particleRenderer = spawnedParticles.GetComponent<ParticleSystemRenderer>();
        particleRenderer.material = spriteRenderer.material;
    }
}
