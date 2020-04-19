using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    void Start()
    {
        var particles = GetComponent<ParticleSystem>();
        if(particles)
        {
            Destroy(this.gameObject, particles.main.duration);
        }
    }
}
