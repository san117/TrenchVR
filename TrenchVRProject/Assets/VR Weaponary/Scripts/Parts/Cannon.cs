using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public class Cannon : WeaponPart
    {
        public GameObject bullet;

        public ParticleSystem shoot_particle;

        [Range(0.01f,1)]
        public float precision = 1;

        public void Shoot()
        {
            shoot_particle.Play();

            var desviation = new Vector3(Random.Range(-20, 20f), Random.Range(-20, 20f), Random.Range(-20, 20f));

            Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(desviation * (1-precision)));
        }
    }
}