using System.Collections.Generic;
using UnityEngine;

public static class EQS
{
    public static readonly Vector2 gridSize = Vector2.one * 2.0f;

    public static Dictionary<Object, System.Func<Vector3>> Blockers { get; } = new Dictionary<Object, System.Func<Vector3>>();

    static Dictionary<Vector3Int, List<EQSResult>> cachedResults { get; } = new Dictionary<Vector3Int, List<EQSResult>>();
    static int lastCacheFrame;

    public static List<EQSResult> QueryEnviromentScores(Vector3 position, float searchRadius, float searchHeight, Object requester) => QueryEnviromentScores(position, searchRadius, searchHeight, requester, new EQSAgentSettings());
    public static List<EQSResult> QueryEnviromentScores(Vector3 position, float searchRadius, float searchHeight, Object requester, EQSAgentSettings agentSettings)
    {
        if (Time.frameCount != lastCacheFrame)
        {
            cachedResults.Clear();
        }
        lastCacheFrame = Time.frameCount;

        Vector3Int cacheKey = Vector3Int.RoundToInt(position);
        if (cachedResults.ContainsKey(cacheKey))
        {
            return cachedResults[cacheKey];
        }

        List<EQSResult> results = new List<EQSResult>();
        float max = 0.0f;

        Vector3 corner = position - new Vector3(searchRadius, 0.0f, searchRadius);
        for (int x = 0; x < searchRadius / gridSize.x * 2.0f + 1.0f; x++)
        {
            for (int z = 0; z < searchRadius / gridSize.y * 2.0f + 1.0f; z++)
            {
                Vector3 center = corner + new Vector3(x * gridSize.x, 0.0f, z * gridSize.y);
                Ray ray = new Ray(center + Vector3.up * searchHeight, Vector3.down);
                if (Physics.SphereCast(ray, agentSettings.agentRadius, out var hit, searchRadius * 2.0f))
                {
                    Vector3 ca = hit.point + Vector3.up * (agentSettings.agentRadius + 0.1f);
                    Vector3 cb = hit.point + Vector3.up * (agentSettings.agentHeight - agentSettings.agentRadius - 0.1f);
                    if (!Physics.CheckCapsule(ca, cb, agentSettings.agentRadius - 0.1f))
                    {
                        EQSResult result = new EQSResult();
                        result.point = hit.point;

                        CalculateScore(ref result, agentSettings, position, requester);
                        max = Mathf.Max(max, result.score);

                        results.Add(result);
                    }
                }
            }
        }

        foreach (var result in results)
        {
            result.score /= max;
        }

        cachedResults.Add(cacheKey, results);
        return results;
    }

    private static void CalculateScore(ref EQSResult result, EQSAgentSettings agentSettings, Vector3 position, Object requester)
    {
        result.score = 1.0f;

        float distance = (result.point - position).magnitude;

        #region Line of Sight
        bool hasLOS = false;
        Vector3 direction = position - (result.point + agentSettings.LOSOffset);
        Ray ray = new Ray(result.point + agentSettings.LOSOffset + direction * agentSettings.LOSDeadRadius, direction);
        if (Physics.Raycast(ray, out var hit, 100.0f, agentSettings.LOSMask))
        {
            if ((position - hit.point).sqrMagnitude < agentSettings.LOSPassDistance * agentSettings.LOSPassDistance)
            {
                hasLOS = true;
            }
        }

        result.score *= hasLOS ? agentSettings.LOSPassMulti : agentSettings.LOSFailMulti;
        #endregion

        #region Deadzone
        if (distance < agentSettings.deadzoneRadius)
        {
            result.score *= agentSettings.deadzoneMulti;
        }
        #endregion

        #region Distance Field
        result.score *= 1.0f / (agentSettings.dfStrength * Mathf.Abs(distance - agentSettings.dfRadius) + 1.0f);
        #endregion

        #region Blockers
        foreach (var blocker in Blockers)
        {
            if (blocker.Key != requester)
            {
                if ((blocker.Value() - result.point).sqrMagnitude < agentSettings.blockerRange * agentSettings.blockerRange)
                {
                    result.score *= agentSettings.blockerScale;
                    break;
                }
            }
        }
        #endregion
    }
}

[System.Serializable]
public class EQSAgentSettings
{
    [Header("AGENT SETTINGS")]
    public float agentHeight = 2.0f;
    public float agentRadius = 0.5f;

    [Space]
    [Header("LINE OF SIGHT SETTINGS")]
    public Vector3 LOSOffset = Vector3.up * 1.5f;
    public float LOSPassMulti = 1.0f;
    public float LOSFailMulti = 0.2f;
    public float LOSPassDistance = 1.0f;
    public float LOSDeadRadius = 1.2f;
    public LayerMask LOSMask = 1;

    [Space]
    [Header("DEADZONE SETTINGS")]
    public float deadzoneRadius = 4.0f;
    public float deadzoneMulti = 0.2f;

    [Space]
    [Header("DISTANCE FIELD SETTINGS")]
    public float dfRadius = 12.0f;
    public float dfStrength = 0.1f;

    [Space]
    [Header("POST PROCESSING")]
    public float scoreMaxThreshold = 0.1f;

    [Space]
    [Header("BLOCKERS")]
    public float blockerRange = 3.0f;
    public float blockerScale = 0.2f;
}

public class EQSResult
{
    public Vector3 point;
    public float score;

    public override string ToString()
    {
        return $"EQS Result\nPoint: {point}\nScore: {score}";
    }
}