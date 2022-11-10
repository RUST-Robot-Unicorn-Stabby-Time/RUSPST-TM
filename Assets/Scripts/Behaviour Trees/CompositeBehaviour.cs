using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeBehaviour : BehaviourBase
{
    public IEnumerable<BehaviourBase> GetChildBehaviours ()
    {
        List<BehaviourBase> childBehaviours = new List<BehaviourBase>();
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out BehaviourBase behaviour))
            {
                childBehaviours.Add(behaviour);
            }
        }
        return childBehaviours;
    }
}
