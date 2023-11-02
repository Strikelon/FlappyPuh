using UnityEngine;

public class PlayerBulletSpawner : ObjectPool
{
    [SerializeField] private PlayerBullet _playerBulletPrefab;

    private void Start()
    {
        Initialize(_playerBulletPrefab.gameObject);
    }

    public PlayerBullet GetBullet()
    {
        var gameObject = GetObject();
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        gameObject.SetActive(true);
        gameObject.transform.position = spawnPoint;
        var playerBullet = gameObject.GetComponent<PlayerBullet>();

        DisableObjectAbroadScreen();

        return playerBullet;
    }

    protected override void DisableObjectAbroadScreen()
    {
        Vector3 leftDisablePoint = Camera.ViewportToWorldPoint(new Vector2(0, 0.5f));
        Vector3 rightDisablePoint = Camera.ViewportToWorldPoint(new Vector2(1, 0.5f));
        Vector3 topDisablePoint = Camera.ViewportToWorldPoint(new Vector2(0.5f, 1));
        Vector3 bottomDisablePoint = Camera.ViewportToWorldPoint(new Vector2(0.5f, 0));

        foreach (var objectGame in Pool)
        {
            if (objectGame.activeSelf == true)
            {
                if (objectGame.transform.position.x < leftDisablePoint.x ||
                    objectGame.transform.position.x > rightDisablePoint.x ||
                    objectGame.transform.position.y > topDisablePoint.y ||
                    objectGame.transform.position.y < bottomDisablePoint.y)
                {
                    objectGame.SetActive(false);
                }
            }
        }
    }
}