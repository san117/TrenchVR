using OVRTouchSample;
using UnityEngine;
using System;

namespace VRWeaponary
{
    public class GrabPoint : WeaponPart
    {
        [Header("Grab Point Parameters")]
        public GameObject hand_pose;

        public Hand CurrentGrabHand { get; protected set; }

        public override void InitPart()
        {
            hand_pose.SetActive(false);
        }

        public override void LocalGetDownButton(Hand hand)
        {
            if (CurrentGrabHand == null)
            {
                hand_pose.SetActive(true);
                CurrentGrabHand = hand;
                CurrentGrabHand.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            }
        }

        public override void HandleGlobalInputs()
        {
            if (CurrentGrabHand != null)
            {
                if (OVRInput.Get(active_button, CurrentGrabHand.m_controller))
                {
                    GlobalGetButton(CurrentGrabHand);
                }

                if (OVRInput.GetDown(active_button, CurrentGrabHand.m_controller))
                {
                    GlobalGetDownButton(CurrentGrabHand);
                }

                if (OVRInput.GetUp(active_button, CurrentGrabHand.m_controller))
                {
                    GlobalGetUpButton(CurrentGrabHand);
                }
            }
        }

        /// <summary>
        /// Called when OVR masked button is pressed.
        /// </summary>
        /// <param name="hand"></param>
        public virtual void GlobalGetButton(Hand hand) { }
        /// <summary>
        /// Called when OVR masked button was pressed in this frame.
        /// </summary>
        /// <param name="hand"></param>
        public virtual void GlobalGetDownButton(Hand hand) { }
        /// <summary>
        /// Called when OVR masked button was unpresed in this frame.
        /// </summary>
        /// <param name="hand"></param>
        public virtual void GlobalGetUpButton(Hand hand)
        {
            if (CurrentGrabHand != null)
            {
                hand_pose.SetActive(false);
                CurrentGrabHand.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                CurrentGrabHand = null;
            }
        }
    }
}