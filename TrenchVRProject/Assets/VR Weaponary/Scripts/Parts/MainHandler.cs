using OVRTouchSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public class MainHandler : Handler
    {
        [Header("Main Handler Parameters")]
        public SlaveHandler secondary_handler;

        public override void UpdatePart()
        {
            if (CurrentGrabHand != null)
            {
                if (secondary_handler.CurrentGrabHand == null)
                {
                    AttachedWeapon.transform.position = CurrentGrabHand.transform.position + posOffset;
                    AttachedWeapon.transform.rotation = CurrentGrabHand.transform.rotation;

                    AttachedWeapon.transform.localRotation = Quaternion.Euler(rOffset + AttachedWeapon.transform.localRotation.eulerAngles);
                }
                else
                {
                    AttachedWeapon.transform.position = CurrentGrabHand.transform.position + posOffset;

                    var rotation = Quaternion.LookRotation(secondary_handler.CurrentGrabHand.transform.position - AttachedWeapon.transform.position);

                    AttachedWeapon.transform.rotation = rotation;

                    AttachedWeapon.transform.LookAt(secondary_handler.CurrentGrabHand.transform.position, CurrentGrabHand.transform.up);

                    AttachedWeapon.transform.Rotate(rOffset,Space.Self);
                }
            }
        }

        public override void LocalGetDownButton(Hand hand)
        {
            base.LocalGetDownButton(hand);

            AttachedWeapon.DisctivePhysics();

            AttachedWeapon.transform.SetParent(null);

            if (hand.m_controller == OVRInput.Controller.LTouch)
            {
                AttachedWeapon.transform.localScale = new Vector3(-1, 1, 1);
                rOffset = -rotationOffset;
            }
            else if (hand.m_controller == OVRInput.Controller.RTouch)
            {
                AttachedWeapon.transform.localScale = new Vector3(1, 1, 1);
                rOffset = rotationOffset;
            }
        }

        public override void GlobalGetUpButton(Hand hand)
        {
            if (CurrentGrabHand != null)
            {
                if(secondary_handler.CurrentGrabHand == null)
                {
                    AttachedWeapon.ActivePhysics(CurrentGrabHand,2,2);
                }
                else
                {
                    AttachedWeapon.DisctivePhysics(secondary_handler.CurrentGrabHand);
                }

                hand_pose.SetActive(false);
                CurrentGrabHand.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                CurrentGrabHand = null;
            }
        }
    }

}