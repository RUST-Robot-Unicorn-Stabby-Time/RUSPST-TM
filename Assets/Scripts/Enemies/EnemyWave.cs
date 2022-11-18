using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    int wave = 0;

    public int EnemiesLeft { get; private set; }

    private void Awake()
    {
        if (transform.childCount <= 0)
        {
            Destroy(gameObject);
            return;
        }

        FindObjectOfType<ExitDoor>().WinConditions.Add(() => wave >= transform.childCount);

        for (int i = 0; i < transform.childCount; i++)
        {
            EnemiesLeft += transform.GetChild(i).childCount;
            transform.GetChild(i).gameObject.SetActive(i == 0);
        }

        transform.GetChild(0).DetachChildren();
    }

    private void OnEnable()
    {
        EnemyActions.EnemyDiedEvent += OnEnemyDied;
        EnemyActions.AllEnemiesDeadEvent += OnAllEnemiesDead;
    }

    private void OnDisable()
    {
        EnemyActions.EnemyDiedEvent -= OnEnemyDied;
        EnemyActions.AllEnemiesDeadEvent -= OnAllEnemiesDead;
    }

    private void OnEnemyDied(EnemyActions enemy)
    {
        EnemiesLeft--;
    }

    private void OnAllEnemiesDead()
    {
        wave++;

        if (wave >= transform.childCount) return;

        var waveContainer = transform.GetChild(wave);
        waveContainer.gameObject.SetActive(true);
        waveContainer.DetachChildren();
    }
}
