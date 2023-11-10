using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _score;

    public int Score => _score;
    public event UnityAction<Enemy> Dying;

    public void Die()
    {
        Dying?.Invoke(this);
        gameObject.SetActive(false);
    }
}