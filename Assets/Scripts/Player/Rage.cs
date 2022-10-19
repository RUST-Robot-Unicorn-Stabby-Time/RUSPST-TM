using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : MonoBehaviour
{
    public float maxRage;
    [Range(0.0f, 1.0f)]public float ragePercent;

    public void AddRage (float amount)
    {
        ragePercent += amount;
        ragePercent = Mathf.Clamp01(ragePercent);
    }

    public void UseRage ()
    {

    }
}
