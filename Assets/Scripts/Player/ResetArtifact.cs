using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetArtifact : MonoBehaviour
{
    [ContextMenu("Reset Artifacts")]
    private void OnEnable()
    {
        ArtifactSelector.ActiveArtifacts.Clear();
        ArtifactSelector.UpdateArtifacts();
    }
}
