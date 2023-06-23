using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private int weaponDamage;
    [SerializeField] private float shootDistance;
    [SerializeField] private ParticleSystem bloodObjectParticles;
    [SerializeField] private ParticleSystem otherObjectParticles;
    [SerializeField] private float fireRate;
    public string WeaponName {get { return weaponName; } }
    public int WeaponDamage { get { return weaponDamage; } }
    public float ShootDistance { get { return shootDistance; } }
    public ParticleSystem BloodObjectParticles { get { return bloodObjectParticles; } }
    public ParticleSystem OtherObjectParticles { get { return otherObjectParticles; } }
    public float FireRate { get { return fireRate; } }

}
