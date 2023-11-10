using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private int _score;

    public UnityAction GameOver;
    public UnityAction<int> ScoreChanged;

    private void Start()
    {
        _mover = GetComponent<PlayerMover>();
    }

    public void Reset()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
        _mover.Reset();
    }

    public void Die()
    {
        GameOver?.Invoke();
    }

    public void IncreaseScore(int aquiredScore)
    {
        _score += aquiredScore;
        ScoreChanged?.Invoke(_score);
    }
}