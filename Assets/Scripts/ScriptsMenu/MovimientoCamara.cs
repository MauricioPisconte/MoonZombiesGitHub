using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public float rotationSpeed = 1f;  // Velocidad de rotaci�n en grados por segundo

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Update()
    {
        // Calcular el �ngulo de rotaci�n basado en la velocidad y el tiempo transcurrido
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Aplicar la rotaci�n al eje Y de la c�mara
        transform.Rotate(0f, rotationAmount, 0f);
    }
}
