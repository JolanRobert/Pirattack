using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class Pooler : MonoBehaviour
    {
        public static Pooler Instance;
    
        private Dictionary<Key, Pool> pools = new ();
        [SerializeField] private List<PoolKey> poolKeys = new ();
    
        [Serializable]
        public class Pool
        {
            public GameObject prefab;
            public Queue<GameObject> queue = new ();

            public int baseCount;
            [HideInInspector] public float baseRefreshSpeed = 5;
            [HideInInspector] public float refreshSpeed = 5;
        }

        [Serializable]
        public class PoolKey
        {
            public Key key;
            public Pool pool;
        }

        void Awake()
        {
            Instance = this;

            InitPools();
            PopulatePools();
        }

        private int i;
        private void InitPools()
        {
            for (i = 0; i < poolKeys.Count; i++)
            {
                pools.Add(poolKeys[i].key, poolKeys[i].pool);
            }
        }
    
        private void PopulatePools()
        {
            foreach (var pool in pools)
            {
                for (i = 0; i < pool.Value.baseCount; i++)
                {
                    AddInstance(pool.Value);
                }           
            }
        }

        private GameObject objectInstance;
        private void AddInstance(Pool pool)
        {
            objectInstance = Instantiate(pool.prefab, transform);
            objectInstance.SetActive(false);
            pool.queue.Enqueue(objectInstance);
        }

        void Start()
        {
            InitRefreshCount();
        }

        private void InitRefreshCount()
        {
            foreach (KeyValuePair<Key,Pool> pool in pools)
            {
                StartCoroutine(RefreshPool(pool.Value,pool.Value.baseRefreshSpeed));
            }
        }

        private IEnumerator RefreshPool(Pool pool, float t)
        {
            yield return new WaitForSeconds(t);

            if (pool.queue.Count < pool.baseCount)
            {
                AddInstance(pool);
                pool.refreshSpeed = pool.baseRefreshSpeed * (pool.queue.Count / pool.baseCount);
            }
        
            StartCoroutine(RefreshPool(pool,pool.refreshSpeed));
        }

        public GameObject Pop(Key key)
        {
            if (pools[key].queue.Count == 0)
            {
                Debug.LogWarning("pull of "+key+"is empty");
                AddInstance(pools[key]);
            }
            objectInstance = pools[key].queue.Dequeue();
            objectInstance.SetActive(true);
        
            return objectInstance;
        }

        public void Depop(Key key, GameObject go)
        {
            pools[key].queue.Enqueue(go);
            go.transform.parent = transform; //Au cas où on a déplacé le GO
            go.SetActive(false);
        }

        public void DelayedDepop(float t, Key key, GameObject go)
        {
            StartCoroutine(DelayedDepopCoroutine(t, key, go));
        }

        private IEnumerator DelayedDepopCoroutine(float t, Key key, GameObject go)
        {
            yield return new WaitForSeconds(t);
            if (!go.activeSelf) yield break;
            Depop(key,go);
        }
    
        public Dictionary<Key, Pool> GetPools()
        {
            return pools;
        }
    }

    public enum Key
    {
        Bullet,
        Cone,
        Wave,
        Bottle,
        FXBottle,
        BasicEnemy,
        EnemyShield,
        BulletImpactVFX,
        PerkZapVFX,
        PerkZapLine,
        PerkIceVFX
    }
}