using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueryEQS : BehaviourBase
{
    [SerializeField] string sourceKey = "Target";
    [SerializeField] string destinationKey = "EQS";

    [Space]
    [SerializeField] float searchRadius = 20.0f;
    [SerializeField] float searchHeight = 5.0f;
    [SerializeField] EQSAgentSettings querySettings = new EQSAgentSettings();
    [SerializeField] [Range(0.0f, 1.0f)]float threshold = 0.9f;

    protected override EvaluationResult OnExecute()
    {
        Vector3 point;
        if (Tree.blackboard.TryGetValue(sourceKey, out Transform pointTransform))
        {
            point = pointTransform.position;
        }
        else if (!Tree.blackboard.TryGetValue(sourceKey, out point)) return EvaluationResult.Failure;

        var scores = EQS.QueryEnviromentScores(point, searchRadius, searchHeight, querySettings);
        Vector3 best = Vector3.zero;
        foreach (var score in scores)
        {
            if (score.score < threshold) continue;

            if ((score.point - Actions.transform.position).sqrMagnitude < (best - Actions.transform.position).sqrMagnitude)
            {
                best = score.point;
            }
        }

        Tree.blackboard.SetValue(destinationKey, point);
        return EvaluationResult.Success;
    }
}
