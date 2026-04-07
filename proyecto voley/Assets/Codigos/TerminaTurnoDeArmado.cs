using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminaTurnoDeArmado : MonoBehaviour
{
    [Header("Cámaras")]
    public GameObject camaraDeArmado;
    public GameObject camaraDeSaque1; // esta empieza desactivada

    void Start()
    {
        // Asegura el estado inicial correcto
        if (camaraDeArmado != null)
            camaraDeArmado.SetActive(true);

        if (camaraDeSaque1 != null)
            camaraDeSaque1.SetActive(false);
    }

    // Este método lo llamas desde el botón
    public void TerminarTurno()
    {
        if (camaraDeArmado != null && camaraDeSaque1 != null)
        {
            camaraDeArmado.SetActive(false);
            camaraDeSaque1.SetActive(true);
        }
    }
}
