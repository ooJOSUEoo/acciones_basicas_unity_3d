using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    public bool shift;
    private Vector3 playerInput;

    public CharacterController player;

    public float speed;
    private Vector3 movePlayer;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpHeight = 4f;
    
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    public bool isOnSlope = false;
    private Vector3 hitNormal;
    public float slideVelocity = 5f;
    public float slopeForceDown = 10f;

    public Animator playerAnimatorController;
    private Animator amongusObject;

    void Start()
    {
        player = GetComponent<CharacterController>();
        Transform figuraTransform = transform.Find("amongus_player");
        mainCamera = GameObject.Find("mainCamera").GetComponent<Camera>();
        // playerAnimatorController = GetComponent<Animator>();
        if (figuraTransform != null)
        {
            // Obtén el componente Animator dentro de "figura"
            playerAnimatorController = figuraTransform.GetComponent<Animator>();

        }
    } 

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        shift = Input.GetAxisRaw("Run") > 0 ? true : false;

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        if (shift){
            speed = 5f;
        }else{
            speed = 2f;
        }

        playerAnimatorController.SetFloat("velocity", playerInput.magnitude * speed);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        movePlayer = movePlayer * speed;

        player.transform.LookAt(player.transform.position + movePlayer);

        setGravity();

        playerSkills();

        player.Move(movePlayer * Time.deltaTime);   
    }

    void camDirection(){
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    void playerSkills(){
        if (player.isGrounded && Input.GetButtonDown("Jump")){
            fallVelocity = jumpHeight;
            movePlayer.y = fallVelocity;
            playerAnimatorController.SetTrigger("jump");
        }
        //cuando precione la tetla C
        if (player.isGrounded && Input.GetButtonDown("Lean")){
            playerAnimatorController.SetTrigger("lean");           
            // player.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }

    void setGravity(){
        if (player.isGrounded){
            fallVelocity = -gravity * Time.deltaTime;
        }else{
            fallVelocity -= gravity * Time.deltaTime;
            playerAnimatorController.SetFloat("fallVelocity", player.velocity.y);
        }
        movePlayer.y = fallVelocity;
        playerAnimatorController.SetBool("isGrounded", player.isGrounded);
        slideDown();
    }

    public void slideDown(){
        isOnSlope = Vector3.Angle(hitNormal, Vector3.up) > player.slopeLimit;
        if (isOnSlope){
            movePlayer.x += ((1f - hitNormal.y)*hitNormal.x )* slideVelocity;
            movePlayer.z += ((1f - hitNormal.y)*hitNormal.z) * slideVelocity;

            movePlayer.y += -slopeForceDown;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit){
        hitNormal = hit.normal;
    }

    private void OnTriggerStay(Collider other){
        if (other.tag == "MovingPlatform"){
            player.transform.SetParent(other.transform);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == "MovingPlatform"){
            player.transform.SetParent(null);
        }
    }

    private void OnAnimatorMove(){
        
    }
}