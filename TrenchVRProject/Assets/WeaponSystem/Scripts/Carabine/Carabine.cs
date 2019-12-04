using OVRTouchSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem.Weapons
{
    public class Carabine : WeaponManager
    {
        public Receiver receiver;

        public GameObject bullet;

        public ParticleSystem shoot_particle;

        public AudioClip shoot_audio;
        public AudioClip runOutAmmo_audio;
        public AudioClip noAmmo_audio;

        public override void OnReleaseHand(Hand hand)
        {
            if (hand.Equals(mainHand))
            {
                mainHand = null;
                transform.SetParent(null);
                ActivePhysics(hand, 1, 1);
            }
        }

        [ContextMenu("Shoot")]
        public override void Shoot()
        {
            if (ammo > 0)
            {
                receiver.Retract();
                source.PlayOneShot(shoot_audio);

                ammo--;

                GameObject bullet_instance = Instantiate(bullet, transform);
                bullet_instance.transform.SetParent(null);
                bullet_instance.SetActive(true);

                OVRInput.SetControllerVibration(0.1f, 1, mainHand.m_controller);
                StartCoroutine(InterruptVibration(0.1f));

                shoot_particle.Play();

                if (ammo == 0)
                {
                    StartCoroutine(AuxiliarSound(0.15f, runOutAmmo_audio));
                }
            }
            else
            {
                source.PlayOneShot(noAmmo_audio);
            }
        }

        IEnumerator InterruptVibration(float delay)
        {
            yield return new WaitForSeconds(delay);

            OVRInput.SetControllerVibration(0, 0, mainHand.m_controller);
        }

        IEnumerator AuxiliarSound(float delay, AudioClip clip)
        {
            yield return new WaitForSeconds(delay);

            source.PlayOneShot(clip);
        }
    }

}