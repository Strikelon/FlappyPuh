using UnityEngine;

public class PlayerBulletSpawner : ObjectPool<PlayerBullet>
{
    [SerializeField] private PlayerBullet _playerBulletPrefab;

    private void Start()
    {
        Initialize(_playerBulletPrefab);
    }

    public PlayerBullet GetBullet()
    {
        var playerBullet = GetObject();
        Vector3 spawnPoint = new Vector3(
                transform.position.x, 
                transform.position.y, 
                transform.position.z
            );
        playerBullet.gameObject.SetActive(true);
        playerBullet.transform.position = spawnPoint;

        DisableObjectAbroadScreen();

        return playerBullet;
    }

    protected override void DisableObjectAbroadScreen()
    {
        Vector3 leftDisablePoint = Camera.ViewportToWorldPoint(new Vector2(0, 0.5f));
        Vector3 rightDisablePoint = Camera.ViewportToWorldPoint(new Vector2(1, 0.5f));
        Vector3 topDisablePoint = Camera.ViewportToWorldPoint(new Vector2(0.5f, 1));
        Vector3 bottomDisablePoint = Camera.ViewportToWorldPoint(new Vector2(0.5f, 0));

        foreach (var playerBullet in Pool)
        {
            if (playerBullet.gameObject.activeSelf == true)
            {
                if (playerBullet.transform.position.x < leftDisablePoint.x ||
                    playerBullet.transform.position.x > rightDisablePoint.x ||
                    playerBullet.transform.position.y > topDisablePoint.y ||
                    playerBullet.transform.position.y < bottomDisablePoint.y)
                {
                    playerBullet.gameObject.SetActive(false);
                }
            }
        }
    }
}