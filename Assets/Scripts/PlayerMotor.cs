using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public PlayerInput input;
    public float horizontalMove;
    public float verticalMove;
    public float speed = 5f;
    public CharacterController player;
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        
    }

    public void FixedUpdate(){
        player.Move(new Vector3(horizontalMove, 0, verticalMove) * speed * Time.deltaTime);
    }
}
