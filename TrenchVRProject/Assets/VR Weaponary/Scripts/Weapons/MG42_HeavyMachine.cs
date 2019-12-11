using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public class MG42_HeavyMachine : Weapon
    {
        [Header("MG42 Parts")]
        public GrabPoint grabPoint;
        public Trigger trigger;
        public Cannon cannon;
        public Receiver receiver;

        [Header("MG42 Components")]
        public ParticleSystem steam_particle;
        public ParticleSystem bullet_particle;
        public Transform weapon_body;
        public Transform pivot;

        [Header("MG42 Parameters")]
        public float turnspeed = 1;
        public int shootsPerMinute = 1200;
        public int currentAmmo = 250;
        public int ammoCapacity = 250;

        [Header("Audios")]
        public AudioClip shoot_audio;
        public AudioClip shoot_noammo_audio;
        public AudioClip reload_audio;

        private float nextShootIn;

        public bool isShooting;

        private readonly float bestPrecision = 0.964f;
        private readonly float worstPrecision = 0.37f;
        private float currentPrecision = 1;

        private int shootsMade;
        
        protected override void OnInitWeapon()
        {
            trigger.OnTrigger += StartShoot;
            trigger.OnUntrigger += StopShoot;
            receiver.onMaxReach = Reload;
        }

        private float GetSteam(float heat)
        {
            var m = (worstPrecision - bestPrecision) / - 1;

            return (heat - bestPrecision + m) / m;
        }

        public void StartShoot()
        {
            isShooting = true;
        }

        public void StopShoot()
        {
            isShooting = false;
        }

        private void Reload()
        {
            currentAmmo = ammoCapacity;

            AudioSource.PlayClipAtPoint(reload_audio, transform.position);
        }

        private void Shoot()
        {
            if (Time.time >= nextShootIn)
            {
                shootsMade++;

                if (currentAmmo > 0)
                {
                    nextShootIn = Time.time + (60 / shootsPerMinute);

                    currentAmmo--;

                    cannon.Shoot();

                    currentPrecision = Mathf.MoveTowards(currentPrecision, worstPrecision, Time.deltaTime * 0.033f);

                    if (shootsMade % 5 == 0)
                    {
                        AudioSource.PlayClipAtPoint(shoot_audio, transform.position);
                    }
                }
                else
                {
                    if (shootsMade % 10 == 0)
                    {
                        AudioSource.PlayClipAtPoint(shoot_noammo_audio, transform.position);
                    }
                }
            }
            
        }

        protected override void UpdateWeapon()
        {
            if (grabPoint.CurrentGrabHand != null)
            {
                var rotation = Quaternion.LookRotation(-(grabPoint.CurrentGrabHand.transform.position - pivot.position));
                pivot.rotation = Quaternion.Slerp(pivot.transform.rotation, rotation, turnspeed * Time.deltaTime);
            }
            else
            {
                isShooting = false;
            }

            var bullets = bullet_particle.emission;

            if (isShooting)
            {
                Shoot();
            }
            else
            {
                bullets.rateOverTime = 0;
            }

            if (!isShooting || currentAmmo <= 0)
            {
                currentPrecision = Mathf.MoveTowards(currentPrecision, bestPrecision, Time.deltaTime * 0.21f);
                bullets.rateOverTime = 0;
            }
            else
            {
                bullets.rateOverTime = 10;
            }

            cannon.precision = currentPrecision;

            var steam_emission = steam_particle.emission;

            steam_emission.rateOverTime = (1-GetSteam(currentPrecision)) * 27;
        }
    }
}