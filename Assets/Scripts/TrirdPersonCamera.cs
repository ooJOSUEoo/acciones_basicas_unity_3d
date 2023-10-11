using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrirdPersonCamera : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 2, -2);
    private Transform target;
    [Range (0, 1)]public float lerpValue = 1f;
    public float sencibility = 4f;
    public float minY = -60f;// Establece el ángulo máximo en Y
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValue);  
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sencibility, Vector3.up) * offset;   
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * sencibility, Vector3.left) * offset;
        offset = Quaternion.AngleAxis(Input.GetAxis("Joystick X") * sencibility, Vector3.up) * offset;
        offset = Quaternion.AngleAxis(Input.GetAxis("Joystick Y") * sencibility, Vector3.left) * offset;
        transform.LookAt(target); 
    }
}
