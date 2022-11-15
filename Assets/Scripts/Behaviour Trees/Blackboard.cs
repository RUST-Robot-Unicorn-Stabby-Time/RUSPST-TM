using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Blackboard : ScriptableObject
{
    public Dictionary<string, object> values { get; } = new Dictionary<string, object>();

    public bool TryGetValue<T>(string key, out T result)
    {
        if (values.ContainsKey(key))
        {
            if (values[key] is T)
            {
                result = (T)values[key];
                return true;
            }
            result = default;
            return false;
        }

        result = default;
        return false;
    }

    public T GetValue<T> (string key, T fallback)
    {
        if (values.ContainsKey(key))
        {
            return (T)values[key];
        }
        return fallback;
    }

    public void SetValue<T> (string key, T value)
    {
        if (values.ContainsKey(key))
        {
            values[key] = value;
        }
        else values.Add(key, value);
    }
    
    public void IncrementValueF (string key, float value)
    {
        if (values.ContainsKey(key))
        {
            values[key] = (float)values[key] + value;
        }
        else
        {
            values.Add(key, value);
        }
    }

    public bool ValueExists(string key) => values.ContainsKey(key);
}
