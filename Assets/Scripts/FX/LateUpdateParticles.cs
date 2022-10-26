using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[DisallowMultipleComponent]
[RequireComponent(typeof(ParticleSystem))]
public class LateUpdateParticles : MonoBehaviour
{
    ParticleSystem system;

    bool reset;

    public void ResetParticles ()
    {
        reset = true;
    }

    private void LateUpdate()
    {
        if (!system) system = GetComponent<ParticleSystem>();

        system.Simulate(Time.deltaTime, true, reset, false);
        reset = false;
    }
}
