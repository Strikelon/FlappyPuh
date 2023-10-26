using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerBulletSpawner _bulletSpawner;

    private void Update()
    {
        transform.position = new Vector3(
            _mover.transform.position.x,
            _mover.transform.position.y,
            transform.position.z
        );

        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_bulletSpawner.TryGetBullet(out PlayerBullet bullet))
        {
            Vector2 moverDirection = new Vector2(_mover.transform.right.x, _mover.transform.right.y);
            Debug.Log($"{moverDirection}");
            bullet.Init(moverDirection);
            bullet.transform.parent = null;
        }
        else
        {
            Debug.Log("false");
        }
    }
}