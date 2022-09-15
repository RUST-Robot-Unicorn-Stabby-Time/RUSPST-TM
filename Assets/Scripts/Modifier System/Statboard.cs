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

    public float GetBaseStat(string statKey)
    {
        foreach (Stat stat in stats)
        {
            if (stat.key == statKey)
            {
                return stat.baseValue;
            }
        }

        throw new System.Exception($"Uh oh, Statboard is missing stat: \"{statKey}\"");
    }

    public float GetStat(string statKey) 
    {
        return finalValues[statKey];
    }

    private void Update()
    {
        foreach (Stat stat in stats)
        {
            if (!finalValues.ContainsKey(stat.key))
            {
                finalValues.Add(stat.key, stat.baseValue);
            }
            else
            {
                finalValues[stat.key] = stat.baseValue;
            }
        }

        foreach (Artifact artifact in artifacts)
        {
            if (artifact) 
                artifact.Apply(this, finalValues);
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

[System.Serializable]
public class Stat
{
    public string key;
    public float baseValue;
}