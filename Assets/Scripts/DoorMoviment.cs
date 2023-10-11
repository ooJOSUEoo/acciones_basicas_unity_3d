using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoorMoviment : MonoBehaviour
{
    public Transform door; // Esto ya lo tienes, donde "door" es el objeto que contiene ambas partes de la puerta.
    public float velocidadApertura = 70f; // Velocidad de apertura de la puerta.
    public Transform der;
    public Transform izq;
    public TextMeshProUGUI textInteract;
    public float maxDistance = 3.0f;
    public bool interact = false;
    public Transform player;
    public bool isOpenDoor = false;

    void Start()
    {
        // Guarda las posición inicial de la puerta.
        door = transform;
        der = door.Find("der");
        izq = door.Find("izq");
        player = GameObject.Find("player").transform; 
        textInteract = GameObject.Find("textInteract").GetComponent<TextMeshProUGUI>();
        textInteract.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        float distanciaDer = Vector3.Distance(player.position, der.position);
        float distanciaIzq = Vector3.Distance(player.position, izq.position);

        if (distanciaDer <= maxDistance || distanciaIzq <= maxDistance)
        {
            // El jugador está lo suficientemente cerca de la puerta para activar el texto.
            interact = true;
            textInteract.gameObject.SetActive(true);

            if (Input.GetButtonDown("Interact") && interact)	
            {
                if (isOpenDoor)
                {
                    der.transform.position -= new Vector3(velocidadApertura * Time.fixedDeltaTime, 0, 0);
                    izq.transform.position += new Vector3(velocidadApertura * Time.fixedDeltaTime, 0, 0);
                    isOpenDoor = false;
                }
                else
                {
                    der.transform.position += new Vector3(velocidadApertura * Time.fixedDeltaTime, 0, 0);
                    izq.transform.position -= new Vector3(velocidadApertura * Time.fixedDeltaTime, 0, 0);
                    isOpenDoor = true;
                }
            }
        }
        else
        {
            interact = false;
            textInteract.gameObject.SetActive(false);
        }
    }
}
