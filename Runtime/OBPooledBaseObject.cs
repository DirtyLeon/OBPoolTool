using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace DirtyWorks.OBPool
{
    [AddComponentMenu("DirtyWorks/OBPool/OBPooledBaseObject")]
    public class OBPooledBaseObject : MonoBehaviour
    {
        public UnityEngine.Events.UnityEvent ReleaseToPoolEvent;
        public System.Action ReleaseToPoolAction;

        public float ReleaseTimeout = 3f;

        private IObjectPool<OBPooledBaseObject> objectPool;
        public IObjectPool<OBPooledBaseObject> ObjectPool { set => objectPool = value; }

        public virtual void ReleaseToPool()
        {
            objectPool.Release(this);
        }

        public void Deactivate()
        {
            StartCoroutine(DeactivateIE(ReleaseTimeout));
        }

        private IEnumerator DeactivateIE(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (gameObject.activeSelf)
            {
                ReleaseToPoolEvent.Invoke();
                ReleaseToPoolAction?.Invoke();
                ReleaseToPool();
            }
        }
    }
}
