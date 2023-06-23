using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public float rotationSpeed = 1f;  // Velocidad de rotación en grados por segundo

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Update()
    {
        // Calcular el ángulo de rotación basado en la velocidad y el tiempo transcurrido
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Aplicar la rotación al eje Y de la cámara
        transform.Rotate(0f, rotationAmount, 0f);
    }
}
