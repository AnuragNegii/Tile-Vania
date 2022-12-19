using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    float gravityScaleAtStart;

    Vector2 moveInput;

    Rigidbody2D myRigidBody;
    Animator myAnimator;

    CapsuleCollider2D myCapsuleCollider2d;
   
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider2d = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart =myRigidBody.gravityScale;
    }
    
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }



    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        // if(myRigidBody.velocity.x > 0 || myRigidBody.velocity.x <0)
        // {
        // myAnimator.SetBool("isRunning", true);
        // }else
        // {
        //     myAnimator.SetBool("isRunning", false);
        // }This is what is done below but in more maths way
    

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);


    }
    
    void FlipSprite()
    {   
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
        transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    void OnJump(InputValue value)
    {   
        if(!myCapsuleCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return;}

        if(value.isPressed){
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

        void ClimbLadder(){
        if(!myCapsuleCollider2d.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {   
            myRigidBody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);

        
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
}
