using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float exitRemainingLifeTime = 0;

    [Header("References")]
    [SerializeField] private new ParticleSystem particleSystem;

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle>particles = new List<ParticleSystem.Particle>();
        if (particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, particles) <= 0)
        {
            return;
        }

        ParticleSystem.Particle[] particlesArray = particles.ToArray();

        for (int i = 0; i < particlesArray.Length; i++)
        {
         if (particlesArray[i].remainingLifetime <= exitRemainingLifeTime) { continue;}
        particlesArray[i].remainingLifetime = exitRemainingLifeTime;
        }
        
            particles = new List<ParticleSystem.Particle>(particlesArray);
            ParticlePhysicsExtensions.SetTriggerParticles(particleSystem, ParticleSystemTriggerEventType.Exit, particles);
    }
}
