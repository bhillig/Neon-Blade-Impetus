using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFinder : MonoBehaviour
{
    public float searchDistance;
    public LayerMask wallMask;

    public WallSearchResult SearchForWall(float wallThreshhold)
    {
        if(Physics.Raycast(
            transform.position, 
            -transform.right, 
            out RaycastHit hitInfo, 
            searchDistance,wallMask))
        {
            var result = ProcessHit(hitInfo, wallThreshhold);
            if (result != null)
                return result;
        }
        if (Physics.Raycast(
            transform.position,
            transform.right,
            out hitInfo,
            searchDistance, wallMask))
        {
            return ProcessHit(hitInfo, wallThreshhold);
        }
        return null;
    }

    private WallSearchResult ProcessHit(RaycastHit hitInfo,float wallThreshhold)
    {
        WallSearchResult result = new WallSearchResult();
        Vector3 normal = hitInfo.normal;
        if (Vector3.Dot(Vector3.up, normal) <= wallThreshhold)
        {
            hitInfo.DrawNormal();
            Vector3 up = Vector3.Cross(normal, transform.forward);
            result.upTangent = up;
            result.norm = normal;
            return result;
        }
        return null;
    }
}

public class WallSearchResult
{
    public Vector3 norm;
    public Vector3 upTangent;    
}
