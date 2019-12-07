using UnityEngine;
using System;

namespace VRWeaponary
{
    public abstract class SimpleWeapon : Weapon
    {
        [Header("Simple Weapon Parameters")]
        public int ammo = 10;
        public int maxAmmo = 10;

        [Header("Parts")]
        
        public Trigger trigger;
        public Cannon cannon;

        [Header("Audios")]
        public AudioClip shoot_audio;
        public AudioClip reload_audio;
        public AudioClip run_out_ammo_audio;
        public AudioClip no_ammo_audio;

        //Events
        public Action onShoot;

        protected override void OnInitWeapon()
        {
            base.OnInitWeapon();

            trigger.OnTrigger += Shoot;
        }

        public void Shoot()
        {
            if (ammo > 0)
            {
                ammo--;

                cannon.Shoot();

                onShoot?.Invoke();

                if (ammo == 0)
                {
                    AudioSource.PlayClipAtPoint(run_out_ammo_audio, transform.position);
                }

                AudioSource.PlayClipAtPoint(shoot_audio, transform.position);
            }
            else
            {
                AudioSource.PlayClipAtPoint(no_ammo_audio, transform.position);
            }
        }

        public void Reload()
        {
            ammo = maxAmmo;
            AudioSource.PlayClipAtPoint(reload_audio, transform.position);
        }
    }
}