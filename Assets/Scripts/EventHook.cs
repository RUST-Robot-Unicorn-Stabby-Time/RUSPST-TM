using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHook : MonoBehaviour
{
    public UnityEvent dealDamageEvent;
    public UnityEvent hitObjectEvent;
    public UnityEvent swingEvent;

    [Space]
    public UnityEvent takeDamageEvent;
    public UnityEvent deathEvent;

    [Space]
    public UnityEvent enterRageEvent;
    public UnityEvent exitRangeEvent;

    PlayerWeapon weapon;

    Rage rage;
    Health health;
    HashSet<HurtBox> hurtBoxes;

    private void OnEnable()
    {
        hurtBoxes = new HashSet<HurtBox>(GetComponentsInChildren<HurtBox>());
        foreach (var hurtbox in hurtBoxes)
        {
            hurtbox.HitEvent += (go, args) => dealDamageEvent?.Invoke();
        }

        if (TryGetComponent(out weapon))
        {
            weapon.BeginAttackEvent += swingEvent.Invoke;
        }

        if (TryGetComponent(out health))
        {
            health.DamageEvent += (args) => takeDamageEvent?.Invoke();
            health.DeathEvent += (args) => deathEvent?.Invoke();
        }

        if (TryGetComponent(out rage))
        {
            rage.RageEnterEvent += enterRageEvent.Invoke;
            rage.RageExitEvent += exitRangeEvent.Invoke;
        }
    }
    
    private void OnDisable()
    {
        foreach (var hurtbox in hurtBoxes)
        {
            hurtbox.HitEvent -= (go, args) => dealDamageEvent?.Invoke();
        }

        if (weapon)
        {
            weapon.BeginAttackEvent -= swingEvent.Invoke;
        }

        if (health)
        {
            health.DamageEvent -= (args) => takeDamageEvent?.Invoke();
            health.DeathEvent -= (args) => deathEvent?.Invoke();
        }

        if (rage)
        {
            rage.RageEnterEvent -= enterRageEvent.Invoke;
            rage.RageExitEvent -= exitRangeEvent.Invoke;
        }
    }
}
