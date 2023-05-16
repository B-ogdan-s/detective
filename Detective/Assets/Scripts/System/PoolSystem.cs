using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem <T> where T : MonoBehaviour
{
    public List<T> _poolObjects = new List<T>();
    private T _poolPrefab;
    private Transform _parents = null;

    public PoolSystem(T PoolPrefab, Transform parents = null)
    {
        _poolPrefab = PoolPrefab;
        _parents = parents;
    }

    public T GetPool()
    {
        foreach (var pool in _poolObjects)
        {
            if(pool.gameObject.activeSelf == false)
            {
                pool.gameObject.SetActive(true);
                return pool;
            }
        }

        T newObject = MonoBehaviour.Instantiate(_poolPrefab);
        newObject.transform.SetParent(_parents);
        newObject.transform.localScale = Vector3.one;
        _poolObjects.Add(newObject);

        return newObject;
    }

    public void DisablePool(T poolObject)
    {
        foreach(var pool in _poolObjects)
        {
            if(pool == poolObject)
            {
                pool.gameObject.SetActive(false);
                return;
            }
        }    
    }

    public void DisablePool()
    {
        foreach (var pool in _poolObjects)
        {
            pool.gameObject.SetActive(false);
        }
    }
}
