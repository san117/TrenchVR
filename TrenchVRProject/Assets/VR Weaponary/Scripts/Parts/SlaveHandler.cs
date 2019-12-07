using OVRTouchSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public class SlaveHandler : GrabPoint
    {
        [Header("Slave Handler Parameters")]
        public Vector3 rotationOffset;
        public Vector3 posOffset;

        public MainHandler main_handler;

        public override void LocalGetDownButton(Hand hand)
        {
            base.LocalGetDownButton(hand);

            if (main_handler.CurrentGrabHand == null)
            {
                AttachedWeapon.DisctivePhysics(hand);
            }
        }

        public override void GlobalGetUpButton(Hand hand)
        {
            if (CurrentGrabHand != null)
            {
                if (main_handler.CurrentGrabHand == null)
                {
                    AttachedWeapon.ActivePhysics(CurrentGrabHand, 2, 2);
                }

                hand_pose.SetActive(false);
                CurrentGrabHand.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                CurrentGrabHand = null;
            }
        }
    }

}