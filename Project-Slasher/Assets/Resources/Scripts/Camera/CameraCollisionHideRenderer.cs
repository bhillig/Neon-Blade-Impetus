using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionHideRenderer : MonoBehaviour
{
    public LayerMask hideMask;
    public Material passthroughMat;

    internal class meshPair
    {
        public meshPair(int count, Material mat)
        {
            this.mat = mat;
            this.count = count;
        }
        public int count = 0;
        public Material mat;
    };

    private Dictionary<Renderer, meshPair> cache = new Dictionary<Renderer, meshPair>();

    private void OnTriggerEnter(Collider other)
    {
        var meshRen = other.GetComponent<Renderer>();
        if (meshRen != null && meshRen.gameObject.IsInLayerMask(hideMask))
        {
            if (!cache.ContainsKey(meshRen))
            {
                cache.Add(meshRen, new meshPair(1, meshRen.material));
                meshRen.material = passthroughMat;
            }
            else
                cache[meshRen].count++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var meshRen = other.GetComponent<Renderer>();
        if (meshRen != null && meshRen.gameObject.IsInLayerMask(hideMask))
        {
            if(cache.ContainsKey(meshRen))
            {
                cache[meshRen].count--;
                if(cache[meshRen].count == 0)
                {
                    meshRen.material = cache[meshRen].mat;
                    cache.Remove(meshRen);
                }
            }
        }
    }
}
