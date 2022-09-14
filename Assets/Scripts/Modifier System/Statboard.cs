using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[SelectionBase]
[DisallowMultipleComponent]
public class Statboard : MonoBehaviour
{
    public List<Stat> stats;
    public List<Artifact> artifacts;

    public Stat GetStat(string statKey) 
    {
        foreach (var stat in stats)
        {
            if (stat.key.ToLower() == statKey.ToLower())
            {
                return stat;
            }
        }

        throw new System.Exception($"Statboard is missing stat \"{statKey}\"");
    }
}

[System.Serializable]
public class Stat
{
    public string key;
    public float baseValue;

    public Stat() { }

    public float Calculate (GameObject ctx, List<Artifact> artifacts)
    {
        float val = baseValue;
        foreach (var artifact in artifacts)
        {
            artifact.Modify(ctx, key, ref val);
        }

        return val;
    }
}