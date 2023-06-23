using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SetInitialZombies : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public int zombiesIniciales;
    [SerializeField] private TextMeshProUGUI textoInicial;

    void Start()
    {
        zombiesIniciales = CantidadInicial.cantidadInicialZombies;
        textoInicial.text = zombiesIniciales.ToString() + " zombies iniciales";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Aumentar1()
    {
        Debug.Log("Esta aumentando");
        if (zombiesIniciales >= 99) zombiesIniciales = 99;
        else zombiesIniciales++;

        CantidadInicial.cantidadInicialZombies = zombiesIniciales;
        textoInicial.text = zombiesIniciales.ToString() + " zombies iniciales";
    }
    public void Disminuir1()
    {
        Debug.Log("Esta disminuyendo");
        if (zombiesIniciales <= 2) zombiesIniciales = 2;
        else zombiesIniciales--;

        CantidadInicial.cantidadInicialZombies = zombiesIniciales;
        textoInicial.text = zombiesIniciales.ToString() + " zombies iniciales";
    }
}
