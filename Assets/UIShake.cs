using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShake : MonoBehaviour
{
    public float damping;
    public float recenter;

    Vector2 offset;
    Vector2 velocity;

    private void LateUpdate()
    {
        RectTransform transform = this.transform as RectTransform;

        velocity -= velocity * damping * Time.deltaTime;
        velocity -= offset * recenter * Time.deltaTime;

        offset += velocity * Time.deltaTime;
        transform.anchoredPosition = offset;
    }

    public void Shake (float magnitude)
    {
        float angle = Random.Range(-Mathf.PI, Mathf.PI);
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        velocity += direction * magnitude;
    }
}
