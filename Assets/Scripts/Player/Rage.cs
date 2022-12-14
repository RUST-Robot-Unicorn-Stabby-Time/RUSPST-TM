using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Rage : MonoBehaviour
{
    public Stat maxRage;
    [Range(0.0f, 1.0f)] public float ragePercent;
    public AnimationCurve rageDecay;
    public Stat rageGain;

    [Space]
    public Volume rageVolume;
    public float rageFXSmoothTime;

    [Space]
    public Transform horn;
    public float baseSize;
    public float flacidSize;
    public float errectSize;

    float rageVel;
    public bool canGainRageInRage = true;

    Statboard statboard;
    bool raging;

    List<StatModification> statMods;
    HashSet<HurtBox> hurtBoxes;

    public event System.Action RageEnterEvent;
    public event System.Action RageExitEvent;

    private void Awake()
    {
        statboard = GetComponent<Statboard>();

        statMods = new List<StatModification>()
        {
            new StatModification("damage", v => v * (raging ? 2.0f : 1.0f)),
            new StatModification("speed", v => v * (raging ? 2.0f : 1.0f)),
            new StatModification("attackspeed", v => v * (raging ? 2.0f : 1.0f)),
        };
    }

    private void OnEnable()
    {
        statboard.RegisterModifications(statMods);
        
        hurtBoxes = new HashSet<HurtBox>(GetComponentsInChildren<HurtBox>());
        foreach (var hurtBox in hurtBoxes)
        {
            hurtBox.HitEvent += (g, args) => AddRage();
        }
    }

    private void OnDisable()
    {
        statboard.DeregisterModifications(statMods);
        foreach (var hurtBox in hurtBoxes)
        {
            hurtBox.HitEvent -= (g, args) => AddRage();
        }
    }

    private void Update()
    {
        rageVolume.weight = Mathf.SmoothDamp(rageVolume.weight, raging ? 1.0f : 0.0f, ref rageVel, rageFXSmoothTime);
        if (horn)
        {
            horn.localScale = new Vector3(baseSize, baseSize, Mathf.Lerp(flacidSize, errectSize, rageVolume.weight));
        }
    }

    public void AddRage()
    {
        if (!canGainRageInRage && raging) return;

        ragePercent += rageGain.GetFor(this) / maxRage.GetFor(this);
        ragePercent = Mathf.Clamp01(ragePercent);
    }

    [ContextMenu("UseRage")]
    public void UseRage()
    {
        if (ragePercent < 0.999f) return;
        if (raging) return;

        StartCoroutine(RageRoutine());
    }

    private IEnumerator RageRoutine()
    {
        raging = true;

        RageEnterEvent.Invoke();

        float upTime = 0.0f;
        while (ragePercent > 0.0f)
        {
            ragePercent -= rageDecay.Evaluate(upTime) / maxRage.GetFor(this) * Time.deltaTime;

            upTime += Time.deltaTime;
            yield return null;
        }

        ragePercent = 0.0f;
        raging = false;

        RageExitEvent.Invoke();
    }
}
