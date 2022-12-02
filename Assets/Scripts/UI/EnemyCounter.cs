using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class EnemyCounter : MonoBehaviour
{
    [SerializeField] string template = "{0}";

    EnemyWave wave;
    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        wave = FindObjectOfType<EnemyWave>();
    }

    private void OnEnable()
    {
        EnemyActions.EnemySpawnedEvent += OnEnemyCountChanged;
        EnemyActions.EnemyDiedEvent += OnEnemyCountChanged;

        OnEnemyCountChanged();
    }

    private void OnDisable()
    {
        EnemyActions.EnemySpawnedEvent -= OnEnemyCountChanged;
        EnemyActions.EnemyDiedEvent -= OnEnemyCountChanged;
    }

    private void OnEnemyCountChanged(EnemyActions obj) => OnEnemyCountChanged();
    private void OnEnemyCountChanged()
    {
        if (!wave) return;
        text.text = string.Format(template, wave.EnemiesLeft - 1);
    }
}
