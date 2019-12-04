using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WeaponSystem
{
    public class Pistol : WeaponManager
    {
        public Animator pistol_anim;
        public ParticleSystem shoot_particle;
        public Magazine magazine;
        public GameObject bullet;

        public AudioClip noAmmo_audio;
        public AudioClip shoot_audio;

        public UnityEvent onRunOutAmmo;
        public UnityEvent onReload;

        public void DropMagazine()
        {
            ammo = 0;
            magazine.Drop();
        }

        [ContextMenu("Shoot")]
        public override void Shoot()
        {
            if (ammo > 0)
            {
                ammo--;
                shoot_particle.Play();
                pistol_anim.SetTrigger("Shoot");
                source.PlayOneShot(shoot_audio);

                GameObject bullet_instance = Instantiate(bullet, transform);
                bullet_instance.transform.SetParent(null);
                bullet_instance.SetActive(true);

                if (ammo == 0)
                {
                    onRunOutAmmo.Invoke();
                }
            }
            else
            {
                source.PlayOneShot(noAmmo_audio);
            }
        }

        public override void Reload(int ammo)
        {
            base.Reload(ammo);

            onReload.Invoke();
        }
    }
}