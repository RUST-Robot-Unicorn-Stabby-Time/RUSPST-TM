using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    [SerializeField] GameObject lockOnEffect;

    public static HashSet<LockOnTarget> Targets { get; } = new HashSet<LockOnTarget>();

    private void OnEnable()
    {
        Targets.Add(this);
    }

    private void OnDisable()
    {
        Targets.Remove(this);
    }

    public void SetTargeted (bool state)
    {
        lockOnEffect.SetActive(state);
    }
}
