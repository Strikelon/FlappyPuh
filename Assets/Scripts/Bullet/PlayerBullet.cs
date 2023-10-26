using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector2 _direction;
    private bool _isInited = false;

    public void Init(Vector2 direction)
    {
        _direction = direction;
        _isInited = true;
    }

    private void Update()
    {
        if (_isInited)
        {
            transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.Die();
        }
    }
}