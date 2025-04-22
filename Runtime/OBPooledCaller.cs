using UnityEngine;
using UnityEngine.Pool;

namespace DirtyWorks.OBPool
{
    [AddComponentMenu("DirtyWorks/OBPool/OBPooledCaller")]
    public class OBPooledCaller : MonoBehaviour
    {
        public UnityEngine.Events.UnityEvent OnGetEvent;

        public OBPooledBaseObject ObjectPrefab;
        public Transform ObjectParent;

        public int PoolDefaultCapacity { get => poolDefaultCapacity; set => poolDefaultCapacity = value; }
        public int PoolMaxSize { get => poolMaxSize; set => poolMaxSize = value; }

        [SerializeField]
        private bool collectionCheck = true;

        [SerializeField]
        private int poolDefaultCapacity = 20, poolMaxSize = 100;

        private IObjectPool<OBPooledBaseObject> prefabPool;

        private void Awake()
        {
            prefabPool = new ObjectPool<OBPooledBaseObject>(CreateObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, poolDefaultCapacity, poolMaxSize);
        }

        public OBPooledBaseObject GetFromPool()
        {
            var getFromPool = prefabPool.Get();
            OnGetEvent.Invoke();
            return getFromPool;
        }

        private OBPooledBaseObject CreateObject()
        {
            OBPooledBaseObject instance =
                (ObjectParent == null) ? Instantiate(ObjectPrefab) : Instantiate(ObjectPrefab, ObjectParent);
            instance.ObjectPool = prefabPool;
            return instance;
        }

        private void OnGetFromPool(OBPooledBaseObject pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(OBPooledBaseObject pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
        }

        private void OnDestroyPooledObject(OBPooledBaseObject pooledObject)
        {
            Destroy(pooledObject.gameObject);
        }
    }
}
