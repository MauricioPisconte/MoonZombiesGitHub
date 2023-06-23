using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    //private ParticleSystem shootPS;
    [SerializeField] private float VidaInicial;
    [SerializeField] private float VidaActual;
    [SerializeField] private Image BarradeVida;
    [SerializeField] private GameObject[] armas;
    private Arma disparoAccion;
    private int indiceArma = 0;

    private Rigidbody mRb;
    private Vector2 mDirection;
    private Vector2 mDeltaLook;
    
    //private GameObject debugImpactSphere;
    //private GameObject bloodObjectParticles;
    //private GameObject otherObjectParticles;

    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    [SerializeField] bool isReadyToJump;

    [SerializeField] Sprite[] caras;
    [SerializeField] Image caraSalud;


    [SerializeField] float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float groundDrag;
    [SerializeField] bool grounded;

    private AudioSource audioS;


    [HideInInspector] public Transform cameraMain;

    private void Awake()
    {
        for(int i = 0; i < armas.Length; i++)
        {
            armas[i].SetActive(false);
        }

        isReadyToJump = true;

        armas[indiceArma].SetActive(true);
        disparoAccion = armas[indiceArma].GetComponent<Arma>();
    }
    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        VidaActual = VidaInicial;
        mRb = GetComponent<Rigidbody>();
        cameraMain = transform.Find("Main Camera");

        //debugImpactSphere = Resources.Load<GameObject>("DebugImpactSphere");
        //bloodObjectParticles = Resources.Load<GameObject>("BloodSplat_FX Variant");
        //otherObjectParticles = Resources.Load<GameObject>("GunShot_Smoke_FX Variant");

        Cursor.lockState = CursorLockMode.Locked;
        InvokeRepeating("RecuperarVida", 3.5f,3.5f);
    }

    private void Update()
    {
        BarradeVida.fillAmount = VidaActual / VidaInicial;
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        SpeedControl();
        if (grounded) mRb.drag = groundDrag;
        else mRb.drag = 0f;
    }

    private void FixedUpdate()
    {
        MovePlayer();

        transform.Rotate(
            Vector3.up,
            turnSpeed * Time.deltaTime * mDeltaLook.x
        );
        cameraMain.GetComponent<CameraMovement>().RotateUpDown(
            -turnSpeed * Time.deltaTime * mDeltaLook.y
        );

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(mRb.velocity.x, 0f, mRb.velocity.z);
        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            mRb.velocity = new Vector3(limitedVel.x, mRb.velocity.y, limitedVel.z);
        }
    }

    private void RecuperarVida()
    {
        if ((VidaActual + 0.5f) >= VidaInicial) VidaActual = VidaInicial;
        else VidaActual += 0.5f;
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = mDirection.y * transform.forward + mDirection.x * transform.right;
        if(grounded) mRb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
        else mRb.AddForce(moveDirection.normalized * speed * 10f * airMultiplier, ForceMode.Force);
    }

    private void OnChange()
    {
        armas[indiceArma].SetActive(false);
        indiceArma++;
        if (indiceArma >= 3) indiceArma = 0;
        armas[indiceArma].SetActive(true);
        disparoAccion = armas[indiceArma].GetComponent<Arma>();
    }

    private void OnMove(InputValue value)
    {
        mDirection = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
        mDeltaLook = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            disparoAccion.Shoot();
        }
    }

    private void OnJump(InputValue value)
    {
        if (isReadyToJump && grounded)
        {
            isReadyToJump = false;
            Saltar();
            Invoke("ReiniciarSalto", jumpCooldown);
        }
    }

    private void Saltar()
    {
        mRb.velocity = new Vector3(mRb.velocity.x, 0f, mRb.velocity.z);

        mRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        audioS.Play();
    }

    private void ReiniciarSalto()
    {
        isReadyToJump = true;
    }

    public void TakeDamage(float damage)
    {
        if (VidaActual - damage > 0)
        {
            VidaActual -= damage;
            SetCara(VidaActual);
        }
        else
        {
            CancelInvoke();
            VidaActual = 0f;
            // Cargar Escena con la ronda alcanzada
            Debug.Log("Fin del juego");
            ChangeScene.CambiarEscena("GameOver");
        }
    }

    private void SetCara(float vida)
    {
        if (vida >= 20f) caraSalud.sprite = caras[0];
        else if(vida < 20f && vida >= 17f) caraSalud.sprite = caras[1];
        else if(vida < 17f && vida >= 14f) caraSalud.sprite = caras[2];
        else if(vida < 14f && vida >= 11f) caraSalud.sprite = caras[3];
        else if(vida < 11f && vida >= 8.5f) caraSalud.sprite = caras[4];
        else if(vida < 8.5f && vida >= 6f) caraSalud.sprite = caras[5];
        else if(vida < 6f && vida >= 4f) caraSalud.sprite = caras[6];
        else if(vida < 4f) caraSalud.sprite = caras[7];

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemigo-Attack"))
        {
            Debug.Log("Player recibio danho");
            TakeDamage(1f);
        }  
    }
}
