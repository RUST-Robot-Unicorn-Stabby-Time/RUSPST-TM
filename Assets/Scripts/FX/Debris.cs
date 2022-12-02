using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    const int maxDebris = 100;

    [SerializeField] float holdTime;
    [SerializeField] float scaleTime;
    [SerializeField] AnimationCurve scaleCurve;

    static List<Debris> debris = new List<Debris>();

    IEnumerator Start()
    {
        for (int i = 0; i < debris.Count - maxDebris; i++)
        {
            Destroy(debris[debris.Count - 1].gameObject);
            debris.RemoveAt(debris.Count - 1);
        }

        yield return new WaitForSeconds(holdTime);

        float percent = 0.0f;
        while (percent < 1.0f)
        {
            transform.localScale = Vector3.one * scaleCurve.Evaluate(percent);
            percent += Time.deltaTime / scaleTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
