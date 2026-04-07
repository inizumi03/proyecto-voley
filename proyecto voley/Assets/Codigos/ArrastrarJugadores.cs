using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrastrarJugadores : MonoBehaviour
{
    private bool arrastrando = false;
    private Vector3 offset;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        // Verifica que el objeto tenga el tag "Ficha"
        if (gameObject.CompareTag("Ficha"))
        {
            arrastrando = true;

            Vector3 mousePos = ObtenerPosMouse();
            offset = transform.position - mousePos;
        }
    }

    void OnMouseUp()
    {
        arrastrando = false;
    }

    void Update()
    {
        if (arrastrando)
        {
            Vector3 mousePos = ObtenerPosMouse();
            transform.position = mousePos + offset;
        }
    }

    Vector3 ObtenerPosMouse()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = cam.WorldToScreenPoint(transform.position).z;
        return cam.ScreenToWorldPoint(mouseScreenPos);
    }
}
