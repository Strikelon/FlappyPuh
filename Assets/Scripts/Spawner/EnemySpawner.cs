using System.Collections;
using UnityEngine;

public class EnemySpawner : ObjectPool<Enemy>
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private float _maxSpawnPositionY;
    [SerializeField] private float _minSpawnPositionY;
    [SerializeField] private Player _player;

    private void Start()
    {
        Initialize(_enemyPrefab);
        StartCoroutine(SpawnEnemies());
    }

    protected override void DisableObjectAbroadScreen()
    {
        Vector3 disablePoint = Camera.ViewportToWorldPoint(new Vector2(0, 0.5f));

        foreach (var enemy in Pool)
        {
            if (enemy.gameObject.activeSelf == true)
            {
                if (enemy.transform.position.x < disablePoint.x)
                {
                    enemy.GetComponent<Enemy>().Dying -= OnEnemyDying;
                    enemy.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;

        _player.IncreaseScore(enemy.Score);
    }

    private IEnumerator SpawnEnemies()
    {
        while (gameObject.activeSelf)
        {
            Enemy enemy = GetObject();

            float spawnPositionY = Random.Range(_minSpawnPositionY, _maxSpawnPositionY);
            Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);
            enemy.gameObject.SetActive(true);
            enemy.GetComponent<Enemy>().Dying += OnEnemyDying;
            enemy.transform.position = spawnPoint;

            DisableObjectAbroadScreen();

            yield return new WaitForSeconds(_secondsBetweenSpawn);
        }
    }
}