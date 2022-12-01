using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EpilepsyWarning : MonoBehaviour
{
    [SerializeField] float scaleFrequency;
    [SerializeField] float scaleAmplitude;

    [Space]
    [SerializeField] float colorScrollSpeed;

    [Space]
    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;

    Vector2 position;
    Vector2 direction;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();

        direction = Vector2.one;
    }

    private void LateUpdate()
    {
        var transform = this.transform as RectTransform;

        transform.localScale = Vector3.one * Mathf.Lerp(1.0f / scaleAmplitude, scaleAmplitude, Mathf.Sin(Time.time * scaleFrequency) * 0.5f + 0.5f);
        image.color = Color.HSVToRGB(Time.time * colorScrollSpeed % 1.0f, 1.0f, 1.0f);

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, Time.time * rotationSpeed);
        transform.localPosition = position;

        position += direction * moveSpeed;
        if (position.x < Screen.width / -2.0f || position.x > Screen.width / 2.0f)
        {
            direction.x = -direction.x;
            position.x = Mathf.Clamp(position.x, Screen.width / -2.0f, Screen.width / 2.0f);
        }

        if (position.y < Screen.height / -2.0f || position.y > Screen.height / 2.0f)
        {
            direction.y = -direction.y;
            position.y = Mathf.Clamp(position.y, Screen.height / -2.0f, Screen.height / 2.0f);
        }
    }
}
