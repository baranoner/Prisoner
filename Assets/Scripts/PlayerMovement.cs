using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Collider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;
    [SerializeField]float runSpeed = 10f;
    [SerializeField]float jumpSpeed = 5f;
    [SerializeField]float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(20f, 20f);
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject Gun;
    Animator myAnimator;
    PlayerInput myInput;
    float gravityScaleAtStart;
    bool isAlive = true;
    

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<Collider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        myInput = GetComponent<PlayerInput>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        
    }

    
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    

    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value){
        
        

        
        if(value.isPressed && myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }
        
    }
    void OnFire(InputValue value){

        if(value.isPressed){

            Instantiate(Bullet, Gun.GetComponent<Transform>().position,Gun.GetComponent<Transform>().rotation);
        }

    }
    void ClimbLadder(){
         bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        
        if(myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            Vector2 playerVerticalVelocity = new Vector2(myRigidbody.velocity.x, climbSpeed*moveInput.y);
            myRigidbody.velocity = playerVerticalVelocity;
            myRigidbody.gravityScale = 0f;
            myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
            
        }
        else{
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
        }
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(runSpeed*moveInput.x, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        

        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        
        
        
        
        
    }
    void FlipSprite(){
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if( playerHasHorizontalSpeed){

        
        transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    void Die(){

        if(myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")) && isAlive){
             myInput.enabled = false;
             isAlive = false;
             myAnimator.SetTrigger("Dying");
             myRigidbody.velocity = deathKick;
             myBodyCollider2D.enabled = false;
             myFeetCollider2D.enabled = false;
             FindObjectOfType<GameSession>().ProcessPlayerDeath();

        }
       

    }
}
