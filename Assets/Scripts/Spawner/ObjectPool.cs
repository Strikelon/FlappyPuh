using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _initialCapacity;

    private GameObject _prefab;

    protected Camera Camera;
    protected List<GameObject> Pool = new List<GameObject>();

    private GameObject CreateObject(GameObject prefab, Transform transform)
    {
        GameObject spawned = Instantiate(prefab, _container.transform);
        spawned.SetActive(false);
        Pool.Add(spawned);
        return spawned;
    }

    protected void Initialize(GameObject prefab)
    {
        Camera = Camera.main;

        for (int i = 0; i < _initialCapacity; i++)
        {
            CreateObject(prefab, _container.transform);
        }
    }

    protected GameObject GetObject()
    {
        var result = Pool.FirstOrDefault(p => p.activeSelf == false);

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
            if (objectGame.activeSelf == true)
            {
                if (objectGame.transform.position.x < disablePoint.x)
                {
                    objectGame.SetActive(false);
                }
            }
        }
    }

    public void RestPool()
    {
        foreach (var objectGame in Pool)
        {
            objectGame.SetActive(false);
        }
    }
}
