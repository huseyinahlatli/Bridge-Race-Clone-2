using System;
using System.Collections.Generic;
using Singleton;
using UnityEngine;

namespace Pool
{
    public class ObjectPool : Singleton<ObjectPool>
    {
        [Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefabs;
            public int size;
        }

        [SerializeField] private List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> PoolDictionary;

        private void Start()
        {
            PoolDictionary = new Dictionary<string, Queue<GameObject>>();
            CreatePool();
        }

        private void CreatePool()
        {
            foreach (var pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int j = 0; j < pool.size; j++)
                {
                    GameObject obj = Instantiate(pool.prefabs);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }
                
                PoolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!PoolDictionary.ContainsKey(tag)) { return null; }
            
            GameObject objectToSpawn = PoolDictionary[tag].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            PoolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
        
        public GameObject SpawnFromPool(string tag)
        {
            if (!PoolDictionary.ContainsKey(tag))
                return null;

            GameObject objectToSpawn = PoolDictionary[tag].Dequeue();
            objectToSpawn.SetActive(true);
            PoolDictionary[tag].Enqueue(objectToSpawn);
            return objectToSpawn;
        }
    }
}
