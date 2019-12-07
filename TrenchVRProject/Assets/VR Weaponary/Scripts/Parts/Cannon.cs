using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public class Cannon : WeaponPart
    {
        public GameObject bullet;

        public ParticleSystem shoot_particle;

        public void Shoot()
        {
            shoot_particle.Play();

            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}