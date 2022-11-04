using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddArtifact : MonoBehaviour
{
    [SerializeField] int[] pool;
    [SerializeField] int amount;

    [ContextMenu("Add Artifact")]
    private void OnEnable()
    {
        for (int i = 0; i < amount; i++)
        {
            ArtifactSelector.ActiveArtifacts.Add(pool[Random.Range(0, pool.Length)]);
        }
        ArtifactSelector.UpdateArtifacts();
    }
}
