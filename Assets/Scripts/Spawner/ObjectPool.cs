using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _initialCapacity;

    private T _prefab;

    protected Camera Camera;
    protected List<T> Pool = new List<T>();

    private T CreateObject(T prefab, Transform transform)
    {
        GameObject spawned = Instantiate(prefab.gameObject, _container.transform);
        spawned.SetActive(false);
        T component = spawned.GetComponent<T>();
        Pool.Add(component);
        return component;
    }

    protected void Initialize(T prefab)
    {
        Camera = Camera.main;

        for (int i = 0; i < _initialCapacity; i++)
        {
            CreateObject(prefab, _container.transform);
        }
    }

    protected T GetObject()
    {
        var result = Pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        if (result == null)
        {
            result = CreateObject(_prefab, _container.transform);
        }

        return result;
    }

    protected virtual void DisableObjectAbroadScreen()
    {
        Vector3 disablePoint = Camera.ViewportToWorldPoint(new Vector2(0, 0.5f));

        foreach (var objectGame in Pool)
        {
            if (objectGame.gameObject.activeSelf == true)
            {
                if (objectGame.transform.position.x < disablePoint.x)
                {
                    objectGame.gameObject.SetActive(false);
                }
            }
        }
    }

    public void RestPool()
    {
        foreach (var objectGame in Pool)
        {
            objectGame.gameObject.SetActive(false);
        }
    }
}
