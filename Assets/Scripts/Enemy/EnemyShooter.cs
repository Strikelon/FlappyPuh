using UnityEngine;

public class EnemyShooter : ObjectPool
{
    [SerializeField] private EnemyBullet _enemyBullet;
    [SerializeField] private float _secondsBetweenShoot;

    private float _elapsedTime = 0;

    private void Start()
    {
        Initialize(_enemyBullet.gameObject);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= _secondsBetweenShoot)
        {
            if (TryGetObject(out GameObject bullet))
            {
                _elapsedTime = 0;

                Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                bullet.SetActive(true);
                bullet.transform.position = spawnPoint;

                DisableObjectAbroadScreen();
            }
        }
    }
}