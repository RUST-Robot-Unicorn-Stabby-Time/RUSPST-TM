using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHook : MonoBehaviour
{
    public UnityEvent dealDamageEvent;
    public UnityEvent takeDamageEvent;

    Health health;
    HashSet<HurtBox> hurtBoxes;

    private void OnEnable()
    {
        hurtBoxes = new HashSet<HurtBox>(GetComponentsInChildren<HurtBox>());
        foreach (var hurtbox in hurtBoxes)
        {
            hurtbox.HitEvent += (go, args) => dealDamageEvent?.Invoke();
        }

        health = GetComponent<Health>();
        health.DamageEvent += (args) => takeDamageEvent?.Invoke();
    }

    private void OnDisable()
    {
        foreach (var hurtbox in hurtBoxes)
        {
            hurtbox.HitEvent -= (go, args) => dealDamageEvent?.Invoke();
        }

        health.DamageEvent -= (args) => takeDamageEvent?.Invoke();
    }
}
