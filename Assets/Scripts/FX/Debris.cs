using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField] float holdTime;
    [SerializeField] float scaleTime;
    [SerializeField] AnimationCurve scaleCurve;

    IEnumerator Start()
    {
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
