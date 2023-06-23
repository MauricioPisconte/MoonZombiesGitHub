using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    [SerializeField] protected Weapon Weapon;
    [SerializeField] private ParticleSystem particulas;
    [SerializeField] private float nextTimeFire = 0f;

    private Transform cameraMain;

    private AudioSource audioDisparo;

    private Animator m_animator;

    private void Start()
    {
        audioDisparo= GetComponent<AudioSource>();
        cameraMain = FindObjectOfType<PlayerController>().GetComponent<PlayerController>().cameraMain;
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }


    public void Shoot()
    {
        if(Time.time >= nextTimeFire + Weapon.FireRate)
        {
            nextTimeFire= Time.time;
            RaycastShoot();
        }
    }

    private void RaycastShoot()
    {
        m_animator.SetTrigger("Shooting");
        audioDisparo.Play();
        particulas.Play();
        if (Weapon.ShootDistance > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraMain.position, cameraMain.forward, out hit, Weapon.ShootDistance))
            {
                if (hit.collider.CompareTag("Enemigos"))
                {
                    var bloodPS = Instantiate(Weapon.BloodObjectParticles, hit.point, Quaternion.identity);
                    Destroy(bloodPS, 2.5f);
                    var enemyController = hit.collider.GetComponent<EnemyController>();
                    enemyController.TakeDamage(1f);
                }
                /*else
                {
                    var otherPS = Instantiate(Weapon.OtherObjectParticles, hit.point, Quaternion.identity);
                    otherPS.GetComponent<ParticleSystem>().Play();
                    Destroy(otherPS, 3f);
                }*/
            }
        }      
    }

}
