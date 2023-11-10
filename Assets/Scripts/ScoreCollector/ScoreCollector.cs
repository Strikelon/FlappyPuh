using UnityEngine;

public class ScoreCollector : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _player.IncreaseScore(enemy.Score);
        }
    }
}