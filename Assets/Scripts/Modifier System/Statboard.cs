using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[SelectionBase]
[DisallowMultipleComponent]
public class Statboard : MonoBehaviour
{
    [SerializeField] List<Stat> stats;

    List<StatModification> statModifications = new List<StatModification>();

    public Dictionary<string, float> FinalValues { get; private set; } = new Dictionary<string, float>();

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
        if (FinalValues.ContainsKey(statKey))
        {
            value = FinalValues[statKey];
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
        return FinalValues[statKey];
    }

    private void Update()
    {
        foreach (Stat stat in stats)
        {
            if (!FinalValues.ContainsKey(stat.key.ToLower()))
            {
                FinalValues.Add(stat.key.ToLower(), stat.baseValue);
            }
            else
            {
                FinalValues[stat.key.ToLower()] = stat.baseValue;
            }
        }

        statModifications.Sort((a, b) => Util.Sign(b.priority - a.priority));

        List<string> statKeys = new List<string>(FinalValues.Keys);
        foreach (var key in statKeys)
        {
            foreach (var modification in statModifications)
            {
                if (modification.key.ToLower() != key.ToLower()) continue;

                FinalValues[key] = modification.modification(FinalValues[key]);
            }
        }
    }

    public void RegisterModifications(IEnumerable<StatModification> modifications)
    {
        statModifications.AddRange(modifications);
    }

    public void DeregisterModifications(IEnumerable<StatModification> modifications)
    {
        statModifications.RemoveAll(q =>
        {
            foreach (var modification in modifications)
            {
                if (q == modification) return true;
            }
            return false;
        });
    }

    public void RegisterModification(StatModification modification)
    {
        statModifications.Add(modification);
    }

    public void DeregisterModification(StatModification modification)
    {
        statModifications.Remove(modification);
    }
}

//public delegate float StatModification(string key, float value);
public class StatModification
{
    public System.Func<float, float> modification;
    public string key;
    public float priority;

    public StatModification(string key, System.Func<float, float> modification, float priority = 0.0f)
    {
        this.modification = modification;
        this.key = key;
        this.priority = priority;
    }
}

[System.Serializable]
public class Stat
{
    public string key;
    public float baseValue;

    public Stat ()
    {
        key = string.Empty;
        baseValue = 0.0f;
    }

    public Stat (string key, float baseValue)
    {
        this.key = key;
        this.baseValue = baseValue;
    }

    public float GetFor (MonoBehaviour ctx)
    {
        Statboard board = ctx.GetComponentInParent<Statboard>();
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