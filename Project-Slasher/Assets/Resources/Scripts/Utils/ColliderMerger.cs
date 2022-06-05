using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class ColliderMerger : MonoBehaviour
{
    private void Awake()
    {
        var meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for(int i = 0;i < meshFilters.Length;i++)
        {
            if (meshFilters[i].gameObject == gameObject) continue;
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = transform.worldToLocalMatrix * meshFilters[i].transform.localToWorldMatrix;
            var meshcoll = meshFilters[i].gameObject.GetComponent<MeshCollider>();
            if (meshcoll != null) meshcoll.convex = true;
            meshFilters[i].gameObject.GetComponent<Collider>().isTrigger = true;
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
