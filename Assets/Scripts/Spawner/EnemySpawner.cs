using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ObjectPool
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private float _maxSpawnPositionY;
    [SerializeField] private float _minSpawnPositionY;
    [SerializeField] private Player _player;

    private float _elapsedTime = 0;

    private void Start()
    {
        Initialize(_prefab);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _secondsBetweenSpawn)
        {
            if (TryGetObject(out GameObject enemy))
            {
                _elapsedTime = 0;

                float spawnPositionY = Random.Range(_minSpawnPositionY, _maxSpawnPositionY);
                Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);
                enemy.SetActive(true);
                enemy.GetComponent<Enemy>().Dying += OnEnemyDying;
                enemy.transform.position = spawnPoint;

                DisableObjectAbroadScreen();
            }
        }
    }

    protected override void DisableObjectAbroadScreen()
    {
        Vector3 disablePoint = Camera.ViewportToWorldPoint(new Vector2(0, 0.5f));

        foreach (var objectGame in Pool)
        {
            if (objectGame.activeSelf == true)
            {
                if (objectGame.transform.position.x < disablePoint.x)
                {
                    objectGame.GetComponent<Enemy>().Dying -= OnEnemyDying;
                    objectGame.SetActive(false);
                }
            }
        }
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;

        _player.IncreaseScore(enemy.Score);
    }
}
