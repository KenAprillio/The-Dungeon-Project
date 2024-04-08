using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static EnemyPooler Instance;

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            PoolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot, bool setActive = true)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("GameObject with tag" + tag + " does not exist!");
            return null;
        }

        GameObject objectToSpawn = PoolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(setActive);
        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rot;

        PoolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
