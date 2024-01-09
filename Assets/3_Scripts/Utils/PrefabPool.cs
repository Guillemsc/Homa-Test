using UnityEngine;
using UnityEngine.Pool;

public sealed class PrefabPool<T> where T : MonoBehaviour
{
    readonly Transform m_parent;
    readonly T m_prefab;
    
    readonly ObjectPool<T> m_pool;
    
    public PrefabPool(
        T prefab,
        Transform parent
        )
    {
        m_prefab = prefab;
        m_parent = parent;
        
        m_pool = new ObjectPool<T>(
            Create
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

    T Create()
    {
        T instance = Object.Instantiate(m_prefab, m_parent);
        
        return instance;
    }
}