using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace VRWeaponary
{
    public class Trigger : WeaponPart
    {
        [Header("Trigger Parameters")]
        public GrabPoint grabPoint;
        public float trigger_threshold = 0.4f;
        public float untrigger_threshold = 0.1f;

        //Events
        public Action OnTrigger;
        public Action OnUntrigger;

        //Cache variables
        private bool triggering;

        [Header("Audios")]
        public AudioClip trigger_audio;
        public AudioClip untrigger_audio;

        public override void UpdatePart()
        {
            base.UpdatePart();

            if (grabPoint != null && grabPoint.CurrentGrabHand != null)
            {
                if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, grabPoint.CurrentGrabHand.m_controller) >= trigger_threshold)
                {
                    Triggering();
                }

                if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, grabPoint.CurrentGrabHand.m_controller) <= untrigger_threshold)
                {
                    Untrigger();
                }
            }
        }

        private void Triggering()
        {
            if (!triggering)
            {
                triggering = true;

                OnTrigger?.Invoke();

                AudioSource.PlayClipAtPoint(trigger_audio, transform.position);
            }
        }

        private void Untrigger()
        {
            if (triggering)
            {
                triggering = false;

                OnUntrigger?.Invoke();

                AudioSource.PlayClipAtPoint(untrigger_audio, transform.position);
            }
        }
    }

}