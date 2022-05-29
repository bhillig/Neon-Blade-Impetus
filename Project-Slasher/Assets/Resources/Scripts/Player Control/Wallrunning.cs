using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wallrunning
{
    PlayerController context;

    // Wallruning config variables
    private LayerMask whatisWall;
    private LayerMask whatisGround;
    private float wallRunForce = 10.0f;
    private float maxWallRunTime = 25.00f;
    private float wallRunTimer = 2.0f;

    // Cooldown
    private float wallRunCooldown = 0.1f;
    private float wallRunCooldownTime;

    // Detection variables
    private float wallCheckDistance = 1.2f;
    private float minJumpHeight = 1.5f;
    private float minWallrunHeightFromGround = 1f;
    private float raycastWaistHeightOffset = 0.75f;

    // temp variables for testing
    private Vector3[] directions;
    private RaycastHit[] hits;
    private float normalizedAngleThreshold = 0.25f;
    private Vector3 lastWallPosition;
    private Vector3 lastWallNormal;
    private bool jumping;
    private float timeSinceLastJumped;
    float elapsedTimeSinceWallAttach = 0;
    float elapsedTimeSinceWallDetatch = 0;

    private Vector3 wallForward;
    private Vector3 wallNormal;
    private bool useGravity = true;
    private float counterGravityForce = 11.0f;

    private float playerRightDotWallNormal;

    public float PlayerRightDotWallNormal => playerRightDotWallNormal;

    //References
    private Transform orientation;
    private Rigidbody rb;

    // Public bool that the wallrunning state machine checks
    public bool isWallRunning;

    public Wallrunning(PlayerController context)
    {
        this.context = context;
        orientation = context.playerPhysicsTransform;
        rb = context.gameObject.GetComponent<Rigidbody>();
        whatisGround = LayerMask.GetMask("Terrain");
        whatisWall = LayerMask.GetMask("Terrain");
        directions = new Vector3[]{
            Vector3.right,
            Vector3.left
        };
    }

    public bool AboveGround(float dist)
    {
        orientation = context.playerPhysicsTransform;
        return !Physics.Raycast(orientation.position, Vector3.down, dist, whatisGround);
    }

    public void StartWallRun()
    {
        isWallRunning = true;
        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
    }


    public void StopWallRun()
    {
        isWallRunning = false;
        rb.velocity -= wallForward * wallRunForce * Time.deltaTime;
    }

    public void CheckDuration()
    {
        wallRunTimer += Time.deltaTime;
        if(wallRunTimer >= maxWallRunTime)
        {
            wallRunTimer = 0.0f;
            StopWallRun();
        }
    }

    public void JumpFromWall(float sideVel, float upVel)
    {
        jumping = true;
        timeSinceLastJumped = 0.0f;

        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
        rb.velocity += GetWallJumpVelocity(sideVel, upVel);
    }

    public bool IsWallRunning()
    {
        return isWallRunning;
    }

    public void SetWallrunCooldown()
    {
        wallRunCooldownTime = Time.time;
    }

    public void DetectWalls(bool performRun = true)
    {
        isWallRunning = false;
        bool wallDetected = false;
        if (CanAttach())
        {
            hits = new RaycastHit[directions.Length];
            var waistHits = new RaycastHit[directions.Length];

            for (int i = 0; i < directions.Length; i++)
            {
                Vector3 dir = orientation.TransformDirection(directions[i]);
                dir = Vector3.ProjectOnPlane(dir, Vector3.up).normalized;
                Physics.Raycast(orientation.position, dir, out hits[i], wallCheckDistance,whatisGround);
                Physics.Raycast(orientation.position + Vector3.up * raycastWaistHeightOffset, 
                    dir, out waistHits[i], wallCheckDistance, whatisGround);
                if (hits[i].collider != null)
                {
                    Debug.DrawRay(orientation.position, dir * hits[i].distance, Color.green);
                }
                else
                {
                    Debug.DrawRay(orientation.position, dir * wallCheckDistance, Color.red);
                }
            }
            if (CanWallRun())
            {
                hits = hits.ToList().Where(h => h.collider != null).OrderBy(h => h.distance).ToArray();
                waistHits = waistHits.ToList().Where(h => h.collider != null).OrderBy(h => h.distance).ToArray();
                if (hits.Length > 0 && waistHits.Length > 0)
                {
                    wallDetected = true;
                    if (performRun)
                        OnWall(hits[0]);
                    lastWallPosition = hits[0].point;
                    lastWallNormal = hits[0].normal;
                }
            }
        }

        if(!wallDetected)
            hits = new RaycastHit[0];

        if (isWallRunning)
        {
            elapsedTimeSinceWallDetatch = 0;
            elapsedTimeSinceWallAttach += Time.deltaTime;
            if(useGravity)
            {
                rb.velocity += Vector3.down * counterGravityForce * Time.deltaTime;
            }
            playerRightDotWallNormal = Vector3.Dot(lastWallNormal, orientation.right);
        }
        else
        {
            elapsedTimeSinceWallAttach = 0;
            elapsedTimeSinceWallDetatch += Time.deltaTime;
        }
    }

    public bool CanWallRun()
    {
        float verticalAxis = context.inputContext.movementInput.y;
        return Time.time - wallRunCooldownTime > wallRunCooldown &&
                rb.velocity.magnitude > 1f && 
                verticalAxis > 0.0f && 
                AboveGround(minWallrunHeightFromGround);
    }

    void OnWall(RaycastHit hit)
    {
        orientation = context.playerPhysicsTransform;
        float d = Vector3.Dot(hit.normal, Vector3.up);
        if (d >= -normalizedAngleThreshold && d <= normalizedAngleThreshold)
        {
            // Vector3 alongWall = Vector3.Cross(hit.normal, Vector3.up);
            float vertical = context.inputContext.movementInput.y;
            Vector3 alongWall = orientation.TransformDirection(Vector3.forward);

            wallNormal = hit.normal;
            wallForward = Vector3.Cross(hit.normal, Vector3.up);
            // Ensures the player wallruns in the direction according to their orientation
            if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            {
                wallForward = -wallForward;
            }
            orientation.forward = wallForward;

            Debug.DrawRay(orientation.position, alongWall.normalized * 10, Color.green);
            Debug.DrawRay(orientation.position, lastWallNormal * 10, Color.magenta);

            rb.velocity = wallForward * wallRunForce * vertical;
            rb.AddForce(-wallNormal * 100.0f, ForceMode.Force);
            isWallRunning = true;
        }
    }

    public bool ShouldWallRun(Vector3 cameraForward)
    {
        // Only enter wall run if camera is pointing towards the wall 
        Vector3 proj = Vector3.ProjectOnPlane(cameraForward, Vector3.up);
        float dot = Vector3.Dot(proj.normalized, lastWallNormal);
        return CanWallRun() && hits.Length > 0 && AboveGround(minJumpHeight) && dot <= 0.2f;
    }

    public bool CanAttach()
    {
/*        if(jumping)
        {
            timeSinceLastJumped += Time.deltaTime;
            if(timeSinceLastJumped >= 0.5f)
            {
                jumping = false;
                timeSinceLastJumped = 0.0f;
            }
            return false;
        }*/
        return true;
    }
    public Vector3 GetWallJumpVelocity(float sideVel, float upVel)
    {
        if (isWallRunning)
        {
            return lastWallNormal * sideVel + Vector3.up * upVel;
        }
        return Vector3.zero;
    }
}
