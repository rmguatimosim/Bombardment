using UnityEngine;

public class Jump : State
{
    private PlayerController controller;
    private bool hasJumped;
    private float cooldown;
    public Jump(PlayerController controller) : base("Jump")
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        //Reset status
        hasJumped = false;
        cooldown = 0.5f;

        //handle animator
        controller.thisAnimator.SetBool("bJumping", true);
    }

    public override void Exit()
    {
        base.Exit();
        //handle animator
        controller.thisAnimator.SetBool("bJumping", false);
    }

    public override void Update()
    {
        base.Update();

        //Update cooldown
        cooldown -= Time.deltaTime;

        //Switch to Idle
        if (hasJumped && controller.isGrounded && cooldown <= 0)
        {
            controller.stateMachine.ChangeState(controller.idleState);
            return;
        }

    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!hasJumped)
        {
            hasJumped = true;
            ApplyImpulse();
        }
        //Create Vector
        Vector3 walkVector = new(controller.movementVector.x, 0, controller.movementVector.y);
        walkVector = controller.GetFoward() * walkVector;
        walkVector *= controller.movementSpeed * controller.jumpMovementFactor;

        //Apply input to character
        controller.thisRigidBody.AddForce(walkVector, ForceMode.Force);

        //Rotate character
        controller.RotateBodyToFaceInput();

    }

    private void ApplyImpulse()
    {
        //Apply impulse
        Vector3 forceVector = Vector3.up * controller.jumpPower;
        controller.thisRigidBody.AddForce(forceVector, ForceMode.Impulse);
    }
    

}