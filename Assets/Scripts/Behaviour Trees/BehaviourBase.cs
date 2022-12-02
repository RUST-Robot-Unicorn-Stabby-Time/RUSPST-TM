using UnityEditor;
using UnityEngine;

[System.Serializable]
public abstract class BehaviourBase : MonoBehaviour
{
    BehaviourTree tree;
    EnemyActions actions;

    public BehaviourTree Tree => tree;
    public EnemyActions Actions => actions;

    protected virtual void Awake()
    {
        tree = GetComponentInParent<BehaviourTree>();
        actions = GetComponentInParent<EnemyActions>();
    }

    public EvaluationResult Execute()
    {
        var result = OnExecute();
        tree.PreviousResults.Add(this, result);
        return result;
    }
    protected abstract EvaluationResult OnExecute();
    public enum EvaluationResult
    { 
        Success = 0,
        Failure = 1,
    }

    public static Color GetResultColor (EvaluationResult result)
    {
        switch (result)
        {
            case EvaluationResult.Success:
                return Color.green;
            case EvaluationResult.Failure:
                return Color.red;
            default:
                return new Color(1.0f, 0.0f, 1.0f);
        }
    }

    protected virtual void Reset()
    {
        name = GetType().ToString();
    }

    public bool GetPointFromBlackboard (string key, out Vector3 point)
    {
        if (Tree.blackboard.TryGetValue(key, out Transform pointTransform))
        {
            point = pointTransform.position;
        }
        else if (!Tree.blackboard.TryGetValue(key, out point)) return false;
        
        return true;
    }

#if UNITY_EDITOR
    static BehaviourBase()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    public static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (obj && Application.isPlaying)
        {
            BehaviourBase behaviour = obj.GetComponent<BehaviourBase>();

            if (behaviour)
            {
                Color color = Color.magenta;
                BehaviourTree tree = obj.GetComponentInParent<BehaviourTree>();
                if (tree)
                {
                    if (tree.PreviousResults.ContainsKey(behaviour))
                    {
                        switch (tree.PreviousResults[behaviour])
                        {
                            case EvaluationResult.Success:
                                color = Color.green;
                                break;
                            case EvaluationResult.Failure:
                                color = Color.red;
                                break;
                        }
                    }
                    else
                    {
                        color = Color.grey;
                    }

                }

                Rect sidebar = new Rect(selectionRect.x + 60.0f - 28.0f - selectionRect.xMin, selectionRect.position.y + 1.0f, 2.0f, selectionRect.size.y - 2.0f);
                EditorGUI.DrawRect(sidebar, color);
            }
        }
    }
#endif
}
