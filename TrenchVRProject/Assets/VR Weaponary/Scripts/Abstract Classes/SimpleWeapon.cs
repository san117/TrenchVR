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
        public Receiver receiver;
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
            receiver.onMaxReach += Reload;
        }

        public void Shoot()
        {
            if (ammo > 0)
            {
                cannon.Shoot();

                onShoot?.Invoke();

                ammo--;

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