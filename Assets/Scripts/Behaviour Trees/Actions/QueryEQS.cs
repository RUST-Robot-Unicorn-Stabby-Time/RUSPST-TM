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

    EQSDebugger debugger;

    protected override EvaluationResult OnExecute()
    {
        Vector3 point;
        if (Tree.blackboard.TryGetValue(sourceKey, out Transform pointTransform))
        {
            point = pointTransform.position;
        }
        else if (!Tree.blackboard.TryGetValue(sourceKey, out point)) return EvaluationResult.Failure;

        var scores = EQS.QueryEnviromentScores(point, searchRadius, searchHeight, Actions, querySettings);
        Vector3? best = null;
        foreach (var score in scores)
        {
            if (score.score < threshold) continue;

            if (!best.HasValue)
            {
                best = score.point;
                continue;
            }

            if ((score.point - Actions.transform.position).sqrMagnitude < (best.Value - Actions.transform.position).sqrMagnitude)
            {
                best = score.point;
            }
        }

        if (!debugger)
        {
            debugger = new GameObject("Debugger").AddComponent<EQSDebugger>();
            debugger.transform.parent = transform;
        }

        debugger.transform.position = point;
        debugger.range = searchRadius;
        debugger.height = searchHeight;
        debugger.agentSettings = querySettings;

        if (best.HasValue)
        {
            Tree.blackboard.SetValue(destinationKey, best.Value);
            return EvaluationResult.Success;
        }
        else
        {
            return EvaluationResult.Failure;
        }
    }
}
