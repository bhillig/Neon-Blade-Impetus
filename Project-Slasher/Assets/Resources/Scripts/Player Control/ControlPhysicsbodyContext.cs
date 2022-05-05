using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPhysicsbodyContext : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private LayerMask groundedMask;
    [SerializeField] private PlayerMovementProfile profile;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SphereCollider groundedCast;

    private int groundContacts = 0;

    private float groundNormalDot = 0f;
    public float GroundNormalDot => groundNormalDot;

    private Vector3 contactNormal;
    private Vector3 ContactNormal => contactNormal;

    private List<RaycastHit> groundedContacts = new List<RaycastHit>();

    private int stepsSinceLastGrounded;
    private bool snappedToGround = false;

    private float snapToGroundBlock = 0f;
    public float SnapToGroundBlock
    {
        set => snapToGroundBlock = value;
    }

    private Vector3 hitSurfacePos; 
    private Vector3 prevHitSurfacePos; 

    private void FixedUpdate()
    {
        //Raycast for data
        List<RaycastHit> hits = new List<RaycastHit>(Physics.SphereCastAll(groundedCast.bounds.center, groundedCast.radius, Vector3.down, 0.02f, groundedMask));
        foreach (var hit in hits)
        {
            EvaluateCollision(hit);
        }
        groundContacts = hits.Count;

        RecalculateNormalsFromContacts();
        // Update state
        UpdateState();
    }

    private void RecalculateNormalsFromContacts()
    {
        //Calculate normals based off previous frames contacts
        prevHitSurfacePos = hitSurfacePos;
        hitSurfacePos = Vector3.zero;
        contactNormal = Vector3.zero;
        foreach (var contact in groundedContacts)
        {
            contactNormal += contact.normal;
            hitSurfacePos += contact.point;
        }
        // Find the average of all the grounded contact normalss
        contactNormal.Normalize();
        if(groundedContacts.Count != 0)
            hitSurfacePos /= groundedContacts.Count;
        groundNormalDot = Vector3.Dot(contactNormal, Vector3.up);
        groundedContacts.Clear();
    }

    private void UpdateState()
    {
        snappedToGround = false;
        stepsSinceLastGrounded++;
        if (IsGrounded() || SnapToGround())
        {
            stepsSinceLastGrounded = 0;
        }
        else
        {
            contactNormal = Vector3.up;
            groundNormalDot = 1f;
        }
    }
    /// <summary>
    /// Snap rigidbody to ground when going over slight bumps
    /// </summary>
    /// <returns></returns>
    private bool SnapToGround()
    {
        if (snapToGroundBlock > 0f)
            snapToGroundBlock -= Time.fixedDeltaTime;
        if (stepsSinceLastGrounded > 1 || snapToGroundBlock > 0f)
        {
            return false;
        }
        if (!Physics.Raycast(groundedCast.bounds.center, 
            Vector3.down, 
            out RaycastHit hit, 
            groundedCast.radius + profile.SnapProbeDistance, 
            groundedMask))
        {
            return false;
        }
        if (hit.normal.y < profile.MinGroundedDotProd)
        {
            return false;
        }
        Vector3 snapNormal = hit.normal;
        Vector3 vel = rb.velocity;
        // Compare to minSnapDotProd
        if (Vector3.Dot(snapNormal.normalized,vel.normalized) <= profile.MaxSnapDotProd)
        {
            snappedToGround = true;
            contactNormal = hit.normal;
            groundNormalDot = Vector3.Dot(contactNormal, Vector3.up);
            //rb.velocity += Vector3.up * (hit.point.y - prevHitSurfacePos.y);
            // Readjust velocity
            if(Vector3.Dot(contactNormal,vel) > 0f)
            {
                vel = ProjectOnContactPlane(vel).normalized * vel.magnitude;
                rb.velocity = vel;
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    private void EvaluateCollision(RaycastHit contact)
    {
        Vector3 normal = contact.normal;
        if (Vector3.Dot(Vector3.up,normal) >= profile.MinGroundedDotProd)
        {
            groundedContacts.Add(contact);
        }
    }
    public bool IsGrounded()
    {
        return (groundContacts > 0 || snappedToGround) && (groundNormalDot >= profile.MinGroundedDotProd);
    }

    public bool IsGroundedRaw()
    {
        return IsGrounded() && (!snappedToGround);
    }

    public Vector3 ProjectOnContactPlane(Vector3 vec)
    {
        return vec - contactNormal * Vector3.Dot(vec, contactNormal);
    }
}
