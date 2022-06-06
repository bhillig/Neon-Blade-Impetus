using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
public class ColliderMerger : MonoBehaviour
{
    private List<Renderer> attachedRenderers = new List<Renderer>();
    public List<Renderer> AttachedRenderers { get => attachedRenderers; }

    private void Awake()
    {
        var meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for(int i = 0;i < meshFilters.Length;i++)
        {
            if (meshFilters[i].gameObject == gameObject) continue;
            var renderer = meshFilters[i].gameObject.GetComponent<Renderer>();
            if (renderer != null)
                attachedRenderers.Add(renderer);
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = transform.worldToLocalMatrix * meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.GetComponent<Collider>().enabled = false;
        }

        var filter = GetComponent<MeshFilter>();
        filter.mesh = new Mesh();
        filter.mesh.CombineMeshes(combine);
        Physics.BakeMesh(filter.mesh.GetInstanceID(), false);
        var coll = GetComponent<MeshCollider>();
        coll.sharedMesh = filter.sharedMesh;
        coll.convex = true;
        coll.enabled = true;
    }

}
