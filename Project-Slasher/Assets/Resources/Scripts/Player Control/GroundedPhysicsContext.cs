using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// <para>Handles physics data when on the ground</para>
/// <para>Mostly taken from Catlike coding's tutorial on movement</para>
/// <para>With moderate changes to be less sus</para>
/// <para>Like who the hell uses OnCollisionStay to do grounded contact checks</para>
/// </summary>
public class GroundedPhysicsContext : MonoBehaviour
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
    public Vector3 ContactNormal => contactNormal;

    private List<RaycastHit> groundedContacts = new List<RaycastHit>();
    private List<RaycastHit> steepContacts = new List<RaycastHit>();

    private int stepsSinceLastGrounded;
    private bool snappedToGround = false;

    private float snapToGroundBlock = 0f;
    public float SnapToGroundBlock
    {
        set => snapToGroundBlock = value;
    }

    private Vector3 hitSurfacePos; 
    private Vector3 prevHitSurfacePos;

    private Vector3 rawNormal;
    public Vector3 RawNormal => rawNormal;

    private void FixedUpdate()
    {
        // Raycast for ground contacts
        List<RaycastHit> hits = new List<RaycastHit>(Physics.SphereCastAll(
            groundedCast.bounds.center, 
            groundedCast.radius, 
            -transform.up, 
            0.03f, 
            groundedMask));

        if (Physics.Raycast(
            groundedCast.bounds.center,
            -transform.up,
            out RaycastHit raw,
            groundedCast.radius + 0.3f,
            groundedMask))
        {
            rawNormal = raw.normal;
        }
        else
            rawNormal = Vector3.up;


        foreach (var hit in hits)
        {
            EvaluateCollision(hit);
        }
        groundContacts = groundedContacts.Count;

        RecalculateNormalsFromContacts();
        // Update state
        UpdateState();
    }

    private void RecalculateNormalsFromContacts()
    {
        // Calculate normals based off previous frame's contacts (this might be an issue cuz its now one fixedupdate behind but whatever)
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
        steepContacts.Clear();
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
        if (Vector3.Dot(snapNormal.normalized,vel.normalized) <= 
            profile.GetMaxSnapDotProd(rb.velocity.magnitude))
        {
            snappedToGround = true;
            contactNormal = hit.normal;
            groundNormalDot = Vector3.Dot(contactNormal, Vector3.up);
            // Readjust velocity if doing so will redirect towards snap target
            if(Vector3.Dot(contactNormal,vel) > 0f)
            {
                vel = Vector3.ProjectOnPlane(vel,contactNormal).normalized * vel.magnitude;
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
        else
        {
            steepContacts.Add(contact);
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

    public void DisplayGroundVectors()
    {
        Vector3 normal = contactNormal;
        // Why work in quaternions when you can do everything with vectors
        // This probably has terrible performance
        Vector3 y = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * Vector3.forward;
        Vector3 cross = Vector3.Cross(rawNormal, y);
        Vector3 forward = Vector3.Cross(rawNormal, cross);
        y.DrawVector(transform.position, 0.5f);
        cross.DrawVector(transform.position, 0.5f);
        normal.DrawVector(transform.position, Color.yellow, 0.5f);
        rawNormal.DrawVector(transform.position, Color.magenta, 0.5f);
        forward.DrawVector(transform.position,Color.cyan, 0.5f);
    }

}
