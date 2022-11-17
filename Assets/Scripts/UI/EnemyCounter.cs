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
        EnemyActions.EnemyDiedEvent += OnEnemyDied;
    }

    private void OnDisable()
    {
        EnemyActions.EnemyDiedEvent -= OnEnemyDied;
    }

    private void OnEnemyDied(EnemyActions obj)
    {
        text.text = string.Format(template, EnemyActions.Enemies.Count);
    }
}
