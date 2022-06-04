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
    private float wallRunForce = 11.75f;
    private float maxWallRunTime = 25.00f;
    private float wallRunTimer = 2.0f;

    // Cooldown
    private float wallRunCooldown;
    private float wallRunCooldownTime;

    // Detection variables
    private float wallCheckDistance = 1.2f;
    private float wallCheckDistanceWaist = 1.6f;
    private float minJumpHeight = 1.25f;
    private float minWallrunHeightFromGround = 1.5f;
    private float raycastWaistHeightOffset = 0.75f;

    // temp variables for testing
    private Vector3[] directions;
    private RaycastHit[] hits;
    private float normalizedAngleThreshold = 0.3f;
    private Vector3 lastWallPosition;
    private Vector3 lastWallNormal;
    private float side;
    public float Side => side;

    public Vector3 LastWallNormal => lastWallNormal;

    private bool jumping;
    private float timeSinceLastJumped;
    float elapsedTimeSinceWallAttach = 0;
    float elapsedTimeSinceWallDetatch = 0;

    private float incomingMagnitude;
    public float IncomingMagnitude
    {
        get { return incomingMagnitude; }
        set { incomingMagnitude = value; }
    }

    private Vector3 wallForward;
    public Vector3 WallForward => wallForward;
    private Vector3 wallDown;

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
            Vector3.right + Vector3.forward * 0.1f,
            Vector3.left + Vector3.forward * 0.1f
        };
    }

    public bool AboveGround(float dist)
    {
        orientation = context.playerPhysicsTransform;
        return !Physics.Raycast(orientation.position, Vector3.down, dist, whatisGround);
    }
    public bool AboveGround(float dist, Vector3 dir)
    {
        orientation = context.playerPhysicsTransform;
        return !Physics.Raycast(orientation.position, dir, dist, whatisGround);
    }

    public void JumpFromWall(float sideVel, float upVel)
    {
        jumping = true;
        timeSinceLastJumped = 0.0f;

        rb.velocity = Vector3.ProjectOnPlane(rb.velocity, lastWallNormal);
        rb.velocity += GetWallJumpVelocity(sideVel, upVel);
    }

    public bool IsWallRunning()
    {
        return isWallRunning;
    }

    public void SetWallrunCooldown(float cooldown)
    {
        wallRunCooldownTime = Time.time;
        wallRunCooldown = cooldown;
    }

    public void DetectWalls(bool performRun = true)
    {
        isWallRunning = false;
        bool wallDetected = false;
        hits = new RaycastHit[directions.Length];
        var waistHits = new RaycastHit[directions.Length];

        for (int i = 0; i < directions.Length; i++)
        {
            Vector3 dir = orientation.TransformDirection(directions[i]);
            dir = Vector3.ProjectOnPlane(dir, Vector3.up).normalized;
            Physics.Raycast(orientation.position, dir, out hits[i], wallCheckDistance,whatisGround);
            Physics.Raycast(orientation.position + Vector3.up * raycastWaistHeightOffset, 
                dir, out waistHits[i], wallCheckDistanceWaist, whatisGround);
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
                Vector3 wallNormal = hits[0].normal;
                Vector3 wallForward = Vector3.Cross(hits[0].normal, Vector3.up);
                wallDown = Vector3.Cross(wallNormal, wallForward);
            }
        }

        if(!wallDetected)
        {
            hits = new RaycastHit[0];
        }

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
        bool enoughSpeed = rb.velocity.magnitude > 1f;
        return Time.time - wallRunCooldownTime > wallRunCooldown &&
                enoughSpeed &&
                verticalAxis > 0.0f && 
                AboveGround(minWallrunHeightFromGround, wallDown);
    }

    void OnWall(RaycastHit hit)
    {
        orientation = context.playerPhysicsTransform;
        float d = Vector3.Dot(hit.normal, Vector3.up);
        if (d >= -normalizedAngleThreshold && d <= normalizedAngleThreshold)
        {
            float vertical = context.inputContext.movementInput.y;
            Vector3 alongWall = orientation.TransformDirection(Vector3.forward);

            wallNormal = hit.normal;
            wallForward = Vector3.Cross(hit.normal, Vector3.up);
            side = 1;
            // Ensures the player wallruns in the direction according to their orientation
            if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            {
                side = -1;
                wallForward = -wallForward;
            }
            /*orientation.rotation = Quaternion.identity;
            orientation.forward = wallForward;*/


            Debug.DrawRay(orientation.position, alongWall.normalized * 10, Color.green);
            Debug.DrawRay(orientation.position, lastWallNormal * 10, Color.magenta);
            float wallRunForceToAdd = wallRunForce;
            if(incomingMagnitude > wallRunForce)
            {
                wallRunForceToAdd = incomingMagnitude;
            }

            rb.velocity = wallForward * wallRunForceToAdd * vertical;
            rb.AddForce(-wallNormal * 50.0f, ForceMode.Force);
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            isWallRunning = true;
        }
    }

    public bool ShouldWallRun(Vector3 cameraForward)
    {
        // Only enter wall run if camera is pointing towards the wall 
        Vector3 proj = Vector3.ProjectOnPlane(cameraForward, Vector3.up);
        float dot = Vector3.Dot(proj.normalized, lastWallNormal);
        return CanWallRun() && hits.Length > 0 && dot <= 0.2f && dot > -0.8f;
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
