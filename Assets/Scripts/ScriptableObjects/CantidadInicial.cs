using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantidadInicial : MonoBehaviour
{
    public static CantidadInicial instance;
    public static int cantidadInicialZombies = 2;

    private void Awake()
    {
        if(CantidadInicial.instance == null)
        {
            CantidadInicial.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
