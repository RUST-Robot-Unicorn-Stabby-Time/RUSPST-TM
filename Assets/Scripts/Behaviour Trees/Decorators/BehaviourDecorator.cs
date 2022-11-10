using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourDecorator : BehaviourBase
{
    public BehaviourBase Child { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Child = transform.GetChild(0)?.GetComponent<BehaviourBase>();
    }
}
