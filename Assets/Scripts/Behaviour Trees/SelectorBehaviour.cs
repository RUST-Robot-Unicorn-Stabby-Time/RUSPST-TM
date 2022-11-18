public class SelectorBehaviour : CompositeBehaviour
{
    protected override EvaluationResult OnExecute()
    {
        foreach (var child in GetChildBehaviours())
        {
            if (child.isActiveAndEnabled)
            if (child.Execute() == EvaluationResult.Success) return EvaluationResult.Success;
        }

        return EvaluationResult.Failure;
    }
}
