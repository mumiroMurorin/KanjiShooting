using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particles;

    // Ä¶
    public void PlayParticle(int index)
    {
        if (particles == null) { return; }
        if (index >= particles.Length) { return; }

        particles[index].Play();
    }
}
