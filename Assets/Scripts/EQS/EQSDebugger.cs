using UnityEditor;
using UnityEngine;

public class EQSDebugger : MonoBehaviour
{
    public float range;
    public float height;

    [Space]
    public EQSAgentSettings agentSettings;

    [Space]
    [SerializeField] bool drawVisuals;
    [SerializeField] bool drawHandles;

    private void OnDrawGizmosSelected()
    {
        float maxScore = float.MinValue;

        var results = EQS.QueryEnviromentScores(transform.position, range, height, agentSettings);

        foreach (var result in results)
        {
            if (result.score > maxScore) maxScore = result.score;
        }

        foreach (var result in results)
        {
            bool best = (maxScore - result.score) < agentSettings.scoreMaxThreshold;

            if (drawVisuals)
            {
                Gizmos.color = best ? Color.blue : Color.Lerp(Color.red, Color.green, result.score / maxScore);
                Gizmos.DrawWireSphere(result.point, best ? 1.0f : result.score / maxScore * 0.5f);
            }

            if (drawHandles)
            {
                Handles.Label(result.point, result.ToString());
            }
        }
    }
}
