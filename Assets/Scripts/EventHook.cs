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

        weapon = GetComponent<PlayerWeapon>();
        weapon.BeginAttackEvent += swingEvent.Invoke;

        health = GetComponent<Health>();
        health.DamageEvent += (args) => takeDamageEvent?.Invoke();
        health.DeathEvent += (args) => deathEvent?.Invoke();

        rage = GetComponent<Rage>();
        rage.RageEnterEvent += enterRageEvent.Invoke;
        rage.RageExitEvent += exitRangeEvent.Invoke;
    }
    
    private void OnDisable()
    {
        foreach (var hurtbox in hurtBoxes)
        {
            hurtbox.HitEvent -= (go, args) => dealDamageEvent?.Invoke();
        }

        weapon.BeginAttackEvent -= swingEvent.Invoke;

        health.DamageEvent -= (args) => takeDamageEvent?.Invoke();
        health.DeathEvent -= (args) => deathEvent?.Invoke();

        rage.RageEnterEvent -= enterRageEvent.Invoke;
        rage.RageExitEvent -= exitRangeEvent.Invoke;
    }
}
