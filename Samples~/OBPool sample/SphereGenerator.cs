using DirtyWorks.OBPool;
using UnityEngine;

public class SphereGenerator : MonoBehaviour
{
    public OBPooledCaller obpool;
    public float overrideForce = 1500f;

    public void Spawn()
    {
        // Get and reference from OBPool.
        PooledSphereBehaviour sphere = obpool.GetFromPool().GetComponent<PooledSphereBehaviour>();

        // Methods to do.
        sphere.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        sphere.ResetSphere();
        sphere.Launch(overrideForce);

        // Call deactivate timer.
        sphere.PooledObject.Deactivate();
    }
}
