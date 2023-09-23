using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    public float speed = 5f;
    private Vector3 movePlayer;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpHeight = 4f;
    
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    void Start()
    {
        player = GetComponent<CharacterController>();
    } 

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

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
        }
    }

    void setGravity(){
        if (player.isGrounded){
            fallVelocity = -gravity * Time.deltaTime;
        }else{
            fallVelocity -= gravity * Time.deltaTime;
        }
            movePlayer.y = fallVelocity;
    }
}
