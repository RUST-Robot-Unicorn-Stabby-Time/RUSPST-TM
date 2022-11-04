using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactSelector : MonoBehaviour
{
    [SerializeField] Transform artifactContainer;

    public static HashSet<int> ActiveArtifacts { get; } = new HashSet<int>();

    public static event System.Action UpdateArtifactsEvent;

    private void Start()
    {
        this.EnableArtifacts();
    }

    private void OnEnable()
    {
        UpdateArtifactsEvent += EnableArtifacts;
    }

    private void OnDisable()
    {
        UpdateArtifactsEvent -= EnableArtifacts;
    }

    public void EnableArtifacts()
    {
        for (int i = artifactContainer.childCount - 1; i >= 0; i--)
        {
            artifactContainer.GetChild(i).gameObject.SetActive(ActiveArtifacts.Contains(i));
        }
    }

    public static void UpdateArtifacts()
    {
        UpdateArtifactsEvent?.Invoke();
    }
}
