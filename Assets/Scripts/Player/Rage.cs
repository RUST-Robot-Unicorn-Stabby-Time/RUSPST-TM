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

    [Space]
    public UnityEvent RageEnterEvent;
    public UnityEvent RageExitEvent;

    float rageVel;
    public bool canGainRageInRage = true;

    Statboard statboard;
    bool raging;

    List<StatModification> statMods;

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
    }

    private void OnDisable()
    {
        statboard.DeregisterModifications(statMods);
    }

    private void Update()
    {
        rageVolume.weight = Mathf.SmoothDamp(rageVolume.weight, raging ? 1.0f : 0.0f, ref rageVel, rageFXSmoothTime);
        horn.localScale = new Vector3(baseSize, baseSize, Mathf.Lerp(flacidSize, errectSize, rageVolume.weight));
    }

    public void AddRage(float amount)
    {
        if (!canGainRageInRage && raging) return;

        ragePercent += amount * rageGain.GetFor(this);
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
