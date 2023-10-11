using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    public float velocidad = 5.0f;
    public float velocidadSprint = 10.0f;
    public float sensibilidad = 2.0f;
    public float fuerzaSalto = 10.0f;
    public float alturaAgachado = 0.5f; // Nueva variable para la altura agachada
    private bool enSuelo;
    private bool agachado = false; // Variable para rastrear si el personaje está agachado

    private Camera cam;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Movimiento del personaje
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        float velocidadActual = Input.GetKey(KeyCode.LeftShift) ? velocidadSprint : velocidad;

        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical) * velocidadActual * Time.deltaTime;
        transform.Translate(movimiento, Space.World);

        // Rotación del personaje en la misma dirección que la cámara
        float rotacionX = Input.GetAxis("Mouse X") * sensibilidad;
        transform.Rotate(Vector3.up * rotacionX, Space.World);

        // Salto
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            enSuelo = false;
        }

        // Agacharse o reducir tamaño al presionar Control
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (agachado)
            {
                // Si ya está agachado, vuelve a la altura original
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                agachado = false;
            }
            else
            {
                // Si no está agachado, reduce la altura
                transform.localScale = new Vector3(1.0f, alturaAgachado, 1.0f);
                agachado = true;
            }
        }

        // Bloquear cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Actualizar la rotación de la cámara
        cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0.0f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
        }
    }
}
