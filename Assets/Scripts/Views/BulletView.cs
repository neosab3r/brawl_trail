using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    public Action<PlayerView> OnTrigger;
    private bool isTriggered = false;
    public List<ParticleSystem> particles = new List<ParticleSystem>();

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered == false)
        {
            if (other.TryGetComponent<PlayerView>(out PlayerView view))
            {
                OnTrigger?.Invoke(view);
            }
            else
            {
                OnTrigger?.Invoke(null);
            }

            isTriggered = true;
        }
    }

    public void ShowParticle()
    {
        foreach (var particle in particles)
        {
            particle.Play();
            particle.enableEmission = true;
        }
    }
}