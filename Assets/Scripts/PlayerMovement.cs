using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f,10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    float gravityScaleAtStart;

    Vector2 moveInput;

    Rigidbody2D myRigidBody;
    Animator myAnimator;

    CapsuleCollider2D myBodyCollider2d;
    BoxCollider2D myFeetCollider;

    SpriteRenderer mySpriteRenderer;

    bool isAlive = true;
   
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2d = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart =myRigidBody.gravityScale;
        myFeetCollider = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if(!isAlive){return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
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
        if(!isAlive){return;}
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return;}

        if(value.isPressed){
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

        void ClimbLadder(){
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
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
        if(!isAlive){return;}
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnFire(InputValue value){
        if(!isAlive){return;}
        Instantiate(bullet, gun.position, transform.rotation);
    }

    void Die(){
        if(myBodyCollider2d.IsTouchingLayers(LayerMask.GetMask("enemies", "Hazards"))){
            isAlive = false;
            mySpriteRenderer.color = new Color(1,0,0,1);
            myAnimator.SetTrigger("Dying");
            myRigidBody.velocity = deathKick;
        }   
    }
    
}
