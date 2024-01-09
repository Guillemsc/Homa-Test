using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

#nullable enable

public sealed class PrefabPool<T> where T : MonoBehaviour
{
    readonly Transform m_parent;
    readonly T m_prefab;
    
    readonly ObjectPool<T> m_pool;

    Action<T>? _whenCreate;
    Action<T>? _whenGet;
    Action<T>? _whenRelease;
    
    public PrefabPool(
        T prefab,
        Transform parent
        )
    {
        m_prefab = prefab;
        m_parent = parent;
        
        m_pool = new ObjectPool<T>(
            WhenCreate,
            WhenGet,
            WhenRelease
        );
    }

    public T Get()
    {
        return m_pool.Get();
    }

    public void Release(T item)
    {
        m_pool.Release(item);
    }
    
    public void SetWhenCreate(Action<T> action)
    {
        _whenCreate = action;
    }

    public void SetWhenGet(Action<T> action)
    {
        _whenGet = action;
    }
    
    public void SetWhenRelease(Action<T> action)
    {
        _whenRelease = action;
    }

    T WhenCreate()
    {
        T instance = Object.Instantiate(m_prefab, m_parent);
        
        _whenCreate?.Invoke(instance);
        
        return instance;
    }

    void WhenGet(T item)
        => _whenGet?.Invoke(item);
    
    void WhenRelease(T item)
    => _whenRelease?.Invoke(item);
}