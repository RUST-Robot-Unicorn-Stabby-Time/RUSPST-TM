using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Rage : MonoBehaviour
{
    public float maxRage;
    [Range(0.0f, 1.0f)] public float ragePercent;
    public AnimationCurve rageDecay;

    [Space]
    public Volume rageVolume;
    public float rageFXSmoothTime;

    [Space]
    public Transform horn;
    public float baseSize;
    public float flacidSize;
    public float errectSize;

    float rageVel;

    Dictionary<string, float> statScaling = new Dictionary<string, float>()
    {
        { "damage", 2.0f },
        { "speed", 2.0f },
        { "attackspeed", 2.0f },
    };
    Statboard statboard;
    bool raging;

    private void Awake()
    {
        statboard = GetComponent<Statboard>();
    }

    private void OnEnable()
    {
        statboard.StatPreProcess += ApplyRage;
    }

    private void Update()
    {
        rageVolume.weight = Mathf.SmoothDamp(rageVolume.weight, raging ? 1.0f : 0.0f, ref rageVel, rageFXSmoothTime);
        horn.localScale = new Vector3(baseSize, baseSize, Mathf.Lerp(flacidSize, errectSize, rageVolume.weight));
    }

    private float ApplyRage(string key, float value)
    {
        if (raging && statScaling.ContainsKey(key))
        {
            return statScaling[key] * value;
        }

        return value;
    }

    private void OnDisable()
    {
        statboard.StatPreProcess -= ApplyRage;
    }

    public void AddRage(float amount)
    {
        ragePercent += amount;
        ragePercent = Mathf.Clamp01(ragePercent);
    }

    public void UseRage()
    {
        if (ragePercent < 0.999f) return;
        if (raging) return;

        StartCoroutine(RageRoutine());
    }

    private IEnumerator RageRoutine()
    {
        raging = true;

        float upTime = 0.0f;
        while (ragePercent > 0.0f)
        {
            ragePercent -= rageDecay.Evaluate(upTime) / maxRage * Time.deltaTime;

            upTime += Time.deltaTime;
            yield return null;
        }

        ragePercent = 0.0f;
        raging = false;
    }
}
