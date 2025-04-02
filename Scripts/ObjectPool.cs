using System.Collections.Generic;
using UnityEngine;
//Author Jerad McManus

/*
* HOW TO USE THE OBJECT POOL
* 
* Spawning an Object (from the Pool)
*    GameObject objectName = ObjectPool.Instance.Spawn(prefab, position, rotation);
* 
* Returning an Object (to the Pool)
*    ObjectPool.Instance.Despawn(myObject);
* 
* Example Usage:
*    GameObject bullet = ObjectPool.Instance.Spawn(bulletPrefab, transform.position, Quaternion.identity);
*    StartCoroutine(ReturnToPoolAfterTime(bullet, 3f));
*    
*    IEnumerator ReturnToPoolAfterTime(GameObject obj, float delay)
*    {
*        yield return new WaitForSeconds(delay);
*        ObjectPool.Instance.Despawn(obj);
*    }
* 
* Why Use This?
* - Optimizes performance by reusing objects instead of constantly instantiating/destroying them.
* - Prevents lag spikes caused by frequent instantiation.
* - Reduces memory fragmentation and garbage collection issues.
* - Easy to implement in most games.
*/

[System.Serializable]
public class PoolItem
{
    public GameObject prefab;
    public int initialSize;
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    [SerializeField] private List<PoolItem> poolItems = new List<PoolItem>();

    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();
    private Dictionary<GameObject, GameObject> spawnedObjects = new Dictionary<GameObject, GameObject>();
    private Dictionary<GameObject, Transform> prefabParents = new Dictionary<GameObject, Transform>();

    private Transform poolParent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        poolParent = new GameObject("ObjectPool_Parent").transform;
        DontDestroyOnLoad(poolParent.gameObject);

        InitializePools();
    }

    private void InitializePools()
    {
        foreach (PoolItem item in poolItems)
        {
            if (item.prefab != null)
                CreateNewPool(item.prefab, item.initialSize);
        }
    }

    private void CreateNewPool(GameObject prefab, int initialSize)
    {
        if (poolDictionary.ContainsKey(prefab))
            return;

        Queue<GameObject> objectPool = new Queue<GameObject>();
        poolDictionary[prefab] = objectPool;

        Transform prefabParent = new GameObject($"{prefab.name}_Pool").transform;
        prefabParent.SetParent(poolParent);
        prefabParents[prefab] = prefabParent;

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab, prefabParent);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    private void ExpandPool(GameObject prefab)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogWarning($"ExpandPool: Pool for '{prefab.name}' doesn't exist. Creating dynamically.");
            CreateNewPool(prefab, 1);
            return;
        }

        Transform prefabParent = prefabParents[prefab];
        GameObject obj = Instantiate(prefab, prefabParent);
        obj.SetActive(false);
        poolDictionary[prefab].Enqueue(obj);
    }

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogWarning($"Spawn: Pool for '{prefab.name}' doesn't exist. Creating dynamically.");
            CreateNewPool(prefab, 1);
        }

        if (poolDictionary[prefab].Count == 0)
            ExpandPool(prefab);

        GameObject obj = poolDictionary[prefab].Dequeue();
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        spawnedObjects[obj] = prefab;

        return obj;
    }

    public void Despawn(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogError("Despawn: Attempted to despawn a null object.");
            return;
        }

        if (!spawnedObjects.TryGetValue(obj, out GameObject prefab))
        {
            Debug.LogError($"Despawn: '{obj.name}' wasn't spawned by this pool system.");
            return;
        }

        obj.SetActive(false);
        obj.transform.SetParent(prefabParents[prefab]);
        poolDictionary[prefab].Enqueue(obj);
        spawnedObjects.Remove(obj);
    }
}