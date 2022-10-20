using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[SelectionBase]
[DisallowMultipleComponent]
public class Statboard : MonoBehaviour
{
    [SerializeField] List<Stat> stats;
    [SerializeField] List<Artifact> artifacts;

    public Dictionary<string, float> finalValues = new Dictionary<string, float>();
    public event StatModification StatPreProcess;
    public event StatModification StatPostProcess;

    public float GetBaseStat(string statKey)
    {
        statKey = statKey.ToLower();
        foreach (Stat stat in stats)
        {
            if (stat.key.ToLower() == statKey.ToLower())
            {
                return stat.baseValue;
            }
        }

        throw new System.Exception($"Uh oh, Statboard is missing stat: \"{statKey}\"");
    }

    public bool TryGetStat(string statKey, out float value)
    {
        statKey = statKey.ToLower();
        if (finalValues.ContainsKey(statKey))
        {
            value = finalValues[statKey];
            return true;
        }
        else
        {
            value = 0.0f;
            return false;
        }
    }

    public float GetStat(string statKey)
    {
        statKey = statKey.ToLower();
        return finalValues[statKey];
    }

    private void Update()
    {
        foreach (Stat stat in stats)
        {
            if (!finalValues.ContainsKey(stat.key.ToLower()))
            {
                finalValues.Add(stat.key.ToLower(), stat.baseValue);
            }
            else
            {
                finalValues[stat.key.ToLower()] = stat.baseValue;
            }
        }

        List<string> keys = new List<string>(finalValues.Keys);
        foreach (var key in keys)
        {
            if (StatPreProcess != null)
            {
                finalValues[key] = StatPreProcess(key, finalValues[key]);
            }
        }

        foreach (Artifact artifact in artifacts)
        {
            if (artifact) 
                artifact.Apply(this, finalValues);
        }

        foreach (var key in keys)
        {
            if (StatPostProcess != null)
            {
                finalValues[key] = StatPostProcess(key, finalValues[key]);
            }
        }
    }

    public void AddArtifact (Artifact artifact)
    {
        artifacts.Add(artifact);
        artifacts.Sort((a, b) => b.priority - a.priority);
    }

    private void OnValidate()
    {
        artifacts.Sort((a, b) =>
        {
            if (!b) return -1;
            if (!a) return 1;
            return b.priority - a.priority;
        });
    }
}

public delegate float StatModification(string key, float value);

[System.Serializable]
public class Stat
{
    public string key;
    public float baseValue;

    public float GetFor (MonoBehaviour ctx)
    {
        Statboard board = ctx.GetComponent<Statboard>();
        float value = baseValue;
        if (board ? board.TryGetStat(key, out value) : false)
        {
            return value;
        }
        else
        {
            return baseValue;
        }
    }
}