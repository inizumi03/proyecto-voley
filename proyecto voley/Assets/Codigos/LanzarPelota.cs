using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzarPelota : MonoBehaviour
{
    [Header("Fuerza")]
    public float multiplicadorFuerza = 15f;
    public float fuerzaMaxima = 30f;

    [Header("Dirección")]
    public float fuerzaHorizontal = 1f;   // eje X (hacia adelante)
    public float fuerzaVertical = 0.5f;   // altura
    public float controlZ = 0.5f;         // qué tanto se desvía en Z

    [Header("Cámaras")]
    public GameObject camaraArmado;
    public GameObject camaraDeSaque1;

    [Header("Visual")]
    public LineRenderer linea;

    private Rigidbody rb;
    private bool arrastrando = false;

    private Vector3 mouseInicial;
    private float fuerzaCalculada = 0f;
    private float desviacionZ = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (linea != null)
            linea.enabled = false;
    }

    void OnMouseDown()
    {
        arrastrando = true;
        rb.isKinematic = true;

        mouseInicial = Input.mousePosition;

        if (linea != null)
            linea.enabled = true;
    }

    void OnMouseUp()
    {
        arrastrando = false;
        rb.isKinematic = false;

        // FUERZA (vertical del mouse)
        float distanciaY = mouseInicial.y - Input.mousePosition.y;
        fuerzaCalculada = distanciaY * multiplicadorFuerza;
        fuerzaCalculada = Mathf.Clamp(fuerzaCalculada, 0, fuerzaMaxima);

        // DIRECCIÓN Z (horizontal del mouse)
        float distanciaX = Input.mousePosition.x - mouseInicial.x;
        desviacionZ = distanciaX * controlZ;

        // DIRECCIÓN FINAL
        Vector3 direccion = new Vector3(
            -fuerzaHorizontal,     // hacia -X
            fuerzaVertical,        // hacia arriba
            desviacionZ            // inclinación en Z
        ).normalized;

        rb.AddForce(direccion * fuerzaCalculada, ForceMode.Impulse);

        if (linea != null)
            linea.enabled = false;

        if (camaraArmado != null && camaraDeSaque1 != null)
        {
            camaraDeSaque1.SetActive(false);
            camaraArmado.SetActive(true);
        }
    }

    void Update()
    {
        if (arrastrando)
        {
            float distanciaY = mouseInicial.y - Input.mousePosition.y;
            distanciaY = Mathf.Clamp(distanciaY, 0, 200);

            fuerzaCalculada = distanciaY * multiplicadorFuerza;
            fuerzaCalculada = Mathf.Clamp(fuerzaCalculada, 0, fuerzaMaxima);

            float distanciaX = Input.mousePosition.x - mouseInicial.x;
            desviacionZ = distanciaX * controlZ;

            DibujarLinea();
        }
    }

    void DibujarLinea()
    {
        if (linea == null) return;

        Vector3 inicio = transform.position;

        Vector3 direccion = new Vector3(
            -fuerzaHorizontal,
            fuerzaVertical,
            desviacionZ
        ).normalized;

        Vector3 fin = inicio + direccion * (fuerzaCalculada * 0.2f);

        linea.SetPosition(0, inicio);
        linea.SetPosition(1, fin);
    }
}
