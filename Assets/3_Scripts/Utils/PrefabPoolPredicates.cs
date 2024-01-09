using UnityEngine;

public static class PrefabPoolPredicates
{
    public static void EnableGameObject<T>(T item) where T : MonoBehaviour
    {
        item.gameObject.SetActive(true);
    }
    
    public static void DisableGameObject<T>(T item) where T : MonoBehaviour
    {
        item.gameObject.SetActive(false);
    }
}