using OVRTouchSample;
using UnityEngine;

namespace VRWeaponary
{
    public class Handler : GrabPoint
    {
        [Header("Handler Parameters")]
        public Vector3 posOffset;
        public Vector3 rotationOffset;

        protected Vector3 rOffset;

        public override void UpdatePart()
        {
            if (CurrentGrabHand != null)
            {
                AttachedWeapon.transform.position = CurrentGrabHand.transform.position + posOffset;
                AttachedWeapon.transform.rotation = CurrentGrabHand.transform.rotation;

                AttachedWeapon.transform.localRotation = Quaternion.Euler(rOffset + AttachedWeapon.transform.localRotation.eulerAngles);
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
                AttachedWeapon.ActivePhysics(CurrentGrabHand, 2, 2);

                hand_pose.SetActive(false);
                CurrentGrabHand.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                CurrentGrabHand = null;
            }
        }
    }

}