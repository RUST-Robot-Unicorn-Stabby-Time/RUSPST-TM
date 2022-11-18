using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class EnemyCounter : MonoBehaviour
{
    [SerializeField] string template = "{0}";

    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        EnemyActions.EnemySpawnedEvent += OnEnemyCountChanged;
        EnemyActions.EnemyDiedEvent += OnEnemyCountChanged;
    }

    private void OnDisable()
    {
        EnemyActions.EnemySpawnedEvent -= OnEnemyCountChanged;
        EnemyActions.EnemyDiedEvent -= OnEnemyCountChanged;
    }

    private void OnEnemyCountChanged(EnemyActions obj)
    {
        text.text = string.Format(template, EnemyActions.Enemies.Count);
    }
}
