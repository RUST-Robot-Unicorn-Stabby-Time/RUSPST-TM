using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeadTeeter : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] Vector2Curve posCurve;
    [SerializeField] AnimationCurve rotCurve;
    [SerializeField] Vector2Curve scaleCurve;
    [SerializeField] RectTransform overrideTransform;

    RectTransform target;
    Vector2 startPos;

    float time;
    bool hover;

    static event System.Action<HeadTeeter> NewHoverEvent;

    private void Awake()
    {
        target = overrideTransform ? overrideTransform : transform as RectTransform;

        startPos = target.anchoredPosition;
    }

    private void OnEnable()
    {
        NewHoverEvent += OnHoverEvent;
    }

    private void OnDisable()
    {
        NewHoverEvent -= OnHoverEvent;
    }

    private void OnHoverEvent(HeadTeeter headTeeter)
    {
        if (headTeeter.gameObject == this.gameObject)
        {
            if (hover) return;

            hover = true;
            time = 0.0f;

            return;
        }

        hover = false;
    }

    private void Update()
    {
        var transform = target;

        if (hover)
        {
            transform.anchoredPosition = startPos + posCurve.Evaluate(time);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotCurve.Evaluate(time));
            transform.localScale = scaleCurve.Evaluate(time);
        }
        else
        {
            transform.anchoredPosition = startPos;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        time += Time.deltaTime;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        NewHoverEvent?.Invoke(this);
    }
}

[System.Serializable]
public class Vector2Curve
{
    public AnimationCurve x, y;

    public Vector2 Evaluate(float t) => new Vector2(x.Evaluate(t), y.Evaluate(t));
}