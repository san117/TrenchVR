using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using UnityEngine;

namespace WeaponSystem
{
    public class Handler : WeaponPart
    {
        public AudioClip hold_audio;

        public bool IsHolding
        {
            get
            {
                return GetHandler != null;
            }
        }

        public Hand GetHandler { get; protected set; }

        protected override void Update()
        {
            base.Update();

            if (IsHolding && OVRInput.GetUp(detect_button, GetHandler.m_controller))
            {
                manager.ReleaseHand(GetHandler);
                GetHandler = null;
            }
        }

        public override void OnHandEnter(Hand hand)
        {
            
        }

        public override void OnHandExit(Hand hand)
        {
           
        }

        /// <summary>
        /// Called when the hand press the acton button inside the trigger.
        /// </summary>
        /// <param name="hand"></param>
        public override void PressedPart(Hand hand)
        {
            if (GetHandler == null)
            {
                GetHandler = hand;
                manager.SetMainHand(hand);

                manager.source.PlayOneShot(hold_audio);

                OVRInput.SetControllerVibration(0.35f,0.1f,hand.m_controller);
                StartCoroutine(InterruptVibration(0.1f));
            }
        }

        IEnumerator InterruptVibration(float delay)
        {
            yield return new WaitForSeconds(delay);

            OVRInput.SetControllerVibration(0, 0, GetHandler.m_controller);
        }

        public override void UnpressingPart(Hand hand)
        {
            
        }

        public override void TriggeringPart(float currentValue, Hand hand)
        {

        }

        public override void PressingPart(Hand hand)
        {
            
        }
    }
}