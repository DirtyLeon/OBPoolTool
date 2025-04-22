using DirtyWorks.OBPool;
using UnityEngine;

public class PooledSphereBehaviour : MonoBehaviour
{
    public Rigidbody rb;

    public OBPooledBaseObject PooledObject;

    public void Launch(float _force)
    {
        rb.AddForce(_force * transform.forward, ForceMode.Acceleration);
    }

    public void ResetSphere()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
