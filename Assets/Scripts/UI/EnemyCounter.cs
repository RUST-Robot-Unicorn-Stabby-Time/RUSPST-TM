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
        EnemyActions.AllEnemiesDeadEvent += OnAllEnemiesDead;
    }


    private void OnDisable()
    {
        EnemyActions.EnemySpawnedEvent -= OnEnemyCountChanged;
        EnemyActions.EnemyDiedEvent -= OnEnemyCountChanged;
        EnemyActions.AllEnemiesDeadEvent -= OnAllEnemiesDead;
    }

    private void OnAllEnemiesDead()
    {
        text.text = string.Format(template, 0);
    }

    private void OnEnemyCountChanged(EnemyActions obj)
    {
        if (!wave) return;
        text.text = string.Format(template, wave.EnemiesLeft);
    }
}
