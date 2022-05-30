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
        public float time = 0f;
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
                // Match colors
                /*                Color c = meshRen.material.GetColor("_Color");
                                MaterialPropertyBlock block = new MaterialPropertyBlock();
                                meshRen.GetPropertyBlock(block);
                                block.SetColor("_Color", c);
                                meshRen.SetPropertyBlock(block);*/
                // Disable shadows to avoid weird dotted shadows
                meshRen.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }
            else
                cache[meshRen].count++;
        }
    }

    private void Update()
    {
        foreach(var pair in cache)
        {
            pair.Value.time += Time.deltaTime;
            MaterialPropertyBlock block = new MaterialPropertyBlock();
            var ren = pair.Key;
            ren.GetPropertyBlock(block);
            float t = Mathf.SmoothStep(0f, 1f, pair.Value.time / 2.5f);
            block.SetFloat("_Alpha", Mathf.Lerp(0.25f, 0.6f, t));
            ren.SetPropertyBlock(block);
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
                    meshRen.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    meshRen.material = cache[meshRen].mat;
                    cache.Remove(meshRen);
                }
            }
        }
    }
}
