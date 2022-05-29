using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionHideRenderer : MonoBehaviour
{
    public LayerMask hideMask;
    public Material passthroughMat;

    private Dictionary<Renderer, Material> cache = new Dictionary<Renderer, Material>();

    private void OnTriggerEnter(Collider other)
    {
        var meshRen = other.GetComponent<Renderer>();
        if (meshRen != null && meshRen.gameObject.IsInLayerMask(hideMask))
        {
            cache.Add(meshRen, meshRen.material);
            meshRen.material = passthroughMat;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var meshRen = other.GetComponent<Renderer>();
        if (meshRen != null && meshRen.gameObject.IsInLayerMask(hideMask))
        {
            meshRen.material = cache[meshRen];
            cache.Remove(meshRen);
        }
    }
}
