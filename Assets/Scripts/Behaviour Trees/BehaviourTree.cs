using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    public Blackboard blackboard;

    [Space]
    public BehaviourBase root;

    public Dictionary<BehaviourBase, BehaviourBase.EvaluationResult> PreviousResults { get; set; } = new Dictionary<BehaviourBase, BehaviourBase.EvaluationResult>();

    public event System.Action PreEvaluationEvent;
    public event System.Action PostEvaluationEvent;

    private void Awake()
    {
        if (!blackboard) blackboard = new Blackboard();
    }

    public void Update()
    {
        if (!root) return;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.RepaintHierarchyWindow();
#endif

        PreEvaluationEvent?.Invoke();

        PreviousResults.Clear();
        root.Execute();

        PostEvaluationEvent?.Invoke();
    }
}
