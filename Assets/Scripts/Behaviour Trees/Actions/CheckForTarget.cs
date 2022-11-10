using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForTarget : BehaviourBase
{
    [SerializeField] float range;
    [SerializeField] string destinationKey;

    protected override EvaluationResult OnExecute()
    {
        foreach (var player in PlayerController.AlivePlayers)
        {
            if ((player.transform.position - Actions.transform.position).sqrMagnitude < range * range)
            {
                Tree.blackboard.SetValue(destinationKey, player.transform);
                return EvaluationResult.Success;
            }
        }

        return EvaluationResult.Failure;
    }
}
