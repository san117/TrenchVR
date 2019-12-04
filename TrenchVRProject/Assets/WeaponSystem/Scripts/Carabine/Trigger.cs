using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using UnityEngine;

namespace WeaponSystem
{
    public class Trigger : Handler
    {
        public AudioSource source;
        private bool pressing;

        public AudioClip trigger_audio;

        public void Shoot()
        {
            if (!pressing)
            {
                pressing = true;

                source.PlayOneShot(trigger_audio);

                manager.Shoot();

                OVRInput.SetControllerVibration(0.1f, 0.1f, GetHandler.m_controller);
                StartCoroutine(InterruptVibration(0.1f));
            }
        }

        IEnumerator InterruptVibration(float delay)
        {
            yield return new WaitForSeconds(delay);

            OVRInput.SetControllerVibration(0, 0, GetHandler.m_controller);
        }

        public override void TriggeringPart(float currentValue, Hand hand)
        {
            if (currentValue > 0.5f)
            {
                Shoot();
            }else
            {
                pressing = false;
            }
        }
    }
}