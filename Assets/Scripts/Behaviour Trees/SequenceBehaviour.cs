public class SequenceBehaviour : CompositeBehaviour
{
    protected override EvaluationResult OnExecute()
    {
        foreach (var child in GetChildBehaviours())
        {
            if (child.isActiveAndEnabled)
            if (child.Execute() == EvaluationResult.Failure) return EvaluationResult.Failure;
        }

        return EvaluationResult.Success;
    }
}
