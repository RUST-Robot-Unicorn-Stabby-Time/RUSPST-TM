using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerHealthUI : HealthUI
{
    [SerializeField] Image portrait;
    [SerializeField] Sprite normalPortrait;
    [SerializeField] Sprite hurtPortrait;
    [SerializeField][Range(0.0f, 1.0f)] float hurtThreshold;

    [Space]
    [SerializeField] Volume hurtVolume;
    [SerializeField] AnimationCurve weightRemap = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 0.0f);

    protected override void Update()
    {
        base.Update();

        if (portrait)
        {
            portrait.sprite = health.currentHealth < hurtThreshold ? hurtPortrait : normalPortrait;
        }

        if (hurtVolume)
        {
            hurtVolume.weight = weightRemap.Evaluate(health.currentHealth);
        }
    }
}
