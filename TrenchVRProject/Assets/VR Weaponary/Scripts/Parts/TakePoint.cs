using OVRTouchSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public class TakePoint : WeaponPart
    {
        public Transform[] grabpoints;

        public Hand CurrentGrabHand { get; protected set; }

        public override void LocalGetDownButton(Hand hand)
        {
            if (CurrentGrabHand == null)
            {
                CurrentGrabHand = hand;
                CurrentGrabHand.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

                AttachedWeapon.DisctivePhysics(hand);
            }
        }

        public override void HandleGlobalInputs()
        {
            if (CurrentGrabHand != null)
            {
                if (OVRInput.GetUp(active_button, CurrentGrabHand.m_controller))
                {
                    AttachedWeapon.ActivePhysics(CurrentGrabHand, 2,1);
                    CurrentGrabHand.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                    CurrentGrabHand = null;
                }
            }
        }

        public override float DistanceToHand(Hand hand)
        {
            var distance = base.DistanceToHand(hand);

            foreach (var grabpoint in grabpoints)
            {
                var d = Vector3.Distance(hand.transform.position, grabpoint.position);

                if (d < distance)
                {
                    distance = d;
                }
            }

            return distance;
        }
    }
}