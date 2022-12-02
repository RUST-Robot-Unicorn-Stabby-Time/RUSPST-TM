using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerHealthUI : HealthUI
{
    [SerializeField] Image portrait;
    [SerializeField] Sprite normalPortrait;
    [SerializeField] Sprite hurtPortrait;
    [SerializeField] Sprite blackFlash;
    [SerializeField] Sprite whiteFlash;
    [SerializeField][Range(0.0f, 1.0f)] float hurtThreshold;

    [Space]
    [SerializeField] Volume hurtVolume;
    [SerializeField] AnimationCurve weightRemap = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 0.0f);
    [SerializeField] AnimationCurve hurtVignetteFlash = AnimationCurve.EaseInOut(0.0f, 1.0f, 0.2f, 0.0f);

    private void OnEnable()
    {
        health.DamageEvent += OnDamage;
    }

    private void OnDisable()
    {
        health.DamageEvent -= OnDamage;
    }

    private void OnDamage(DamageArgs args)
    {
        portrait.sprite = blackFlash;
    }

    protected override void Update()
    {
        base.Update();

        if (portrait)
        {
            if (portrait.sprite == blackFlash) portrait.sprite = whiteFlash;
            else portrait.sprite = health.currentHealth < hurtThreshold ? hurtPortrait : normalPortrait;
        }

        if (hurtVolume)
        {
            hurtVolume.weight = Mathf.Max(weightRemap.Evaluate(health.currentHealth), hurtVignetteFlash.Evaluate(Time.time - health.LastDamageTime));
        }
    }
}
