using System.Collections.Generic;
using UnityEngine;

#nullable enable

public sealed class TowerTilesPool
{
    static TowerTilesPool? m_instance;
    public static TowerTilesPool Instance => m_instance ??= new TowerTilesPool();
    
    readonly Dictionary<TowerTileType, PrefabPool<TowerTile>> m_tilePrefabPools = new();

    readonly Transform m_TilesPoolParent;
    
    TowerTilesPool()
    {
        m_TilesPoolParent = new GameObject("Tiles").transform;
        Object.DontDestroyOnLoad(m_TilesPoolParent);
    }
    
    public PrefabPool<TowerTile> GetOrCreatePoolForTowerTilePrefab(TowerTile prefab)
    {
        bool poolFound = m_tilePrefabPools.TryGetValue(prefab.TileType, out PrefabPool<TowerTile> pool);

        if (!poolFound)
        {
            pool = new PrefabPool<TowerTile>(prefab, m_TilesPoolParent);
            pool.SetWhenCreate(PrefabPoolPredicates.DisableGameObject);
            pool.SetWhenGet(PrefabPoolPredicates.EnableGameObject);
            pool.SetWhenRelease(PrefabPoolPredicates.DisableGameObject);
            
            m_tilePrefabPools.Add(prefab.TileType, pool);
        }

        return pool!;
    }
    
    public PrefabPool<TowerTile>? GetPoolForTowerTileType(TowerTileType type)
    {
        bool poolFound = m_tilePrefabPools.TryGetValue(type, out PrefabPool<TowerTile> pool);

        if (!poolFound)
        {
            return null;
        }

        return pool;
    }
}