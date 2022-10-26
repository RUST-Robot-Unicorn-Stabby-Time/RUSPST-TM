using System.Collections.Generic;
using UnityEngine;

public abstract class Artifact : MonoBehaviour
{
    public string displayName;
    public Sprite icon;
    public Color baseColor;
    public int priority;

    protected Statboard Statboard { get; private set; }

    public List<StatModification> modifications = new List<StatModification>();

    protected virtual void Awake()
    {
        Statboard = GetComponentInParent<Statboard>();
    }

    protected virtual void OnEnable ()
    {
        Statboard.RegisterModifications(modifications);
    }

    protected virtual void OnDisable()
    {
        Statboard.DeregisterModifications(modifications);
    }
}