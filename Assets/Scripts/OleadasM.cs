using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OleadasM : MonoBehaviour
{
    [SerializeField] private GameObject[] puntosSpawn;
    [SerializeField] private GameObject[] enemigos;
    [SerializeField] private int waveCount;
    [SerializeField] private int wave;
    [SerializeField] public int cantidadPorRound;
    public static int puntuacion;

    [SerializeField] private TextMeshProUGUI TextoPuntuacion;
    [SerializeField] private TextMeshProUGUI TextoRonda;
    [SerializeField] private TextMeshProUGUI TextoNextRound;
    private float timer = 0;
    //[SerializeField] private bool spawning;
    private int enemiesSpawned;

    private AudioSource audioS;
    //private GameManager gameManager;
    private void Awake()
    {
        
    }

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        cantidadPorRound = CantidadInicial.cantidadInicialZombies;
        waveCount = cantidadPorRound;
        wave = 0;
        puntuacion = 0;
        //spawning = false;
        enemiesSpawned = 0;
        //gameManager
        timer = 60;

        StartCoroutine(RoundGeneration(60));
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) timer= 0;

        TextoPuntuacion.text = "Score: " + puntuacion.ToString();
        TextoRonda.text = "Round: " + wave.ToString();
        TextoNextRound.text = "Next Round: " + timer.ToString("f1");

        
    }

    private IEnumerator RoundGeneration(int segundos)
    {
        StartCoroutine(SpawnWave(waveCount));
        audioS.Play();
        yield return new WaitForSeconds(segundos);
        if (segundos > 10)
        {
            timer = segundos - 5;
            StartCoroutine(RoundGeneration(segundos - 5));
        }

        else 
        {
            timer = segundos;
            StartCoroutine(RoundGeneration(segundos));
        }


    }

    private IEnumerator SpawnWave(int waveC)
    {
        //spawning = true;
        wave++;
        //sonido y cambio con animacion de ronda
        for(int i = 0; i < waveC; i++)
        {
            int spawnPos = Random.Range(0, 4);
            Instantiate(enemigos[Random.Range(0,1)],
                        puntosSpawn[spawnPos].transform.position,
                        puntosSpawn[spawnPos].transform.rotation);
            enemiesSpawned++;
            yield return new WaitForSeconds(1);
        }
        
        waveCount *= 2;
        //spawning = false;
        //yield break;
    }

}
