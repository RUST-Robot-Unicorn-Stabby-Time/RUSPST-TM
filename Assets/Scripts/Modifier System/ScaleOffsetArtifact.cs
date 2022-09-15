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
            stats[modification.key] *= modification.scale;
            stats[modification.key] += modification.offset;
        }
    }
}

[System.Serializable]
public class ScaleOffsetModification
{
    public string key = string.Empty;
    public float scale = 1.0f;
    public float offset = 0.0f;
}
