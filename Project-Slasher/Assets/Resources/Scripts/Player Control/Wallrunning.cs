using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrunning
{
    PlayerController context;

    // Wallruning config variables
    private LayerMask whatisWall;
    private LayerMask whatisGround;
    private float wallRunForce = 1.5f;
    private float maxWallRunTime = 2.0f;
    private float wallRunTimer = 0.0f;

    // Detection variables
    private float wallCheckDistance = 1.0f;
    private float minJumpHeight = 1.5f;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool leftWall;
    private bool rightWall;
    private Vector3 wallForward;
    private Vector3 wallNormal;

    //References
    private Transform orientation;
    private Rigidbody rb;

    // Public bool that the wallrunning state machine checks
    public bool isWallRunning;

    public Wallrunning(PlayerController context)
    {
        this.context = context;
        orientation = context.gameObject.transform;
        rb = context.gameObject.GetComponent<Rigidbody>();
        whatisGround = LayerMask.GetMask("Terrain");
        whatisWall = LayerMask.GetMask("Terrain");
    }

    public void CheckForWall()
    {
        rightWall = Physics.Raycast(orientation.position, orientation.right, out rightWallHit, wallCheckDistance, whatisWall);
        leftWall = Physics.Raycast(orientation.position, -orientation.right, out leftWallHit, wallCheckDistance, whatisWall);

        // If the player isn't currently next to a wall and is wallrunning, stop
        if(!(rightWall || leftWall) && isWallRunning)
        {
            StopWallRun();
        }
    }

    public bool AboveGround()
    {
        return !Physics.Raycast(orientation.position, Vector3.down, minJumpHeight, whatisGround);
    }

    public void StartWallRun()
    {
        isWallRunning = true;
    }

    public void WallRunningMovement()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        wallNormal = rightWall ? rightWallHit.normal : leftWallHit.normal;
        wallForward = Vector3.Cross(wallNormal, orientation.up);

        // Ensures the player wallruns in the direction according to their orientation
        if((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        rb.velocity += wallForward * wallRunForce * Time.deltaTime;
        //rb.AddForce(wallForward * wallRunForce, ForceMode.Force);
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

    public void JumpFromWall()
    {
        Vector3 direction = -wallNormal + Vector3.up;
        rb.AddForce(direction * 100.0f);
    }

    public bool IsWallRunning()
    {
        return isWallRunning;
    }
}
