using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Artifacts/Scale Offset")]
public class ScaleOffsetArtifact : Artifact
{
    public List<ScaleOffsetModification> modifications;

    public override void Apply(Statboard ctx, Dictionary<string, float> stats)
    {
        foreach (ScaleOffsetModification modification in modifications)
        {
            stats[modification.Key] *= modification.scale;
            stats[modification.Key] += modification.offset;
        }
    }

    public override void Register(Statboard ctx) { }

    public override void Unregister(Statboard ctx) { }
}

[System.Serializable]
public class ScaleOffsetModification
{
    [SerializeField] string key = string.Empty;
    public float scale = 1.0f;
    public float offset = 0.0f;

    public string Key => key.ToLower();
}
