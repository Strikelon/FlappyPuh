using System.Collections;
using UnityEngine;

public class EnemyShooter : ObjectPool<EnemyBullet>
{
    [SerializeField] private EnemyBullet _enemyBullet;
    [SerializeField] private float _secondsBetweenShoot;

    private Coroutine bulletSpawnCoroutine;

    private void OnEnable()
    {
        Initialize(_enemyBullet);
        bulletSpawnCoroutine = StartCoroutine(SpawnBullet());
    }

    private void OnDisable()
    {
        if (bulletSpawnCoroutine != null)
        {
            StopCoroutine(bulletSpawnCoroutine);
        }
    }

    private IEnumerator SpawnBullet()
    {
        while (gameObject.activeSelf)
        {
            EnemyBullet bullet = GetObject();

            Vector3 spawnPoint = new Vector3(
                    transform.position.x, 
                    transform.position.y, 
                    transform.position.z
                );
            bullet.gameObject.SetActive(true);
            bullet.transform.position = spawnPoint;

            DisableObjectAbroadScreen();

            yield return new WaitForSeconds(_secondsBetweenShoot);
        }
    }
}