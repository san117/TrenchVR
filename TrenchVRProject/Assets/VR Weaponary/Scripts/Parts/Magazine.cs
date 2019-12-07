using UnityEngine;
using System;

namespace VRWeaponary
{
    public class Magazine : WeaponPart
    {
        [Header("Magazine")]
        public Handler handler;

        public float ejectionForce = 6;

        public Rigidbody magazine_item;
        private Vector3 originPos;
        private Quaternion originRot;

        private bool loaded = true;

        //Events
        public Action onDropMagazine;
        public Action onRestoreMagazine;

        public override void InitPart()
        {
            originPos = magazine_item.transform.localPosition;
            originRot = magazine_item.transform.localRotation;
        }

        public void DropMagazine()
        {
            if (loaded)
            {
                loaded = false;
                magazine_item.isKinematic = false;
                magazine_item.transform.SetParent(null);

                magazine_item.AddRelativeForce(Vector3.down * ejectionForce);

                magazine_item.velocity = AttachedWeapon.trackingSpace.rotation * OVRInput.GetLocalControllerVelocity(handler.CurrentGrabHand.m_controller) * 2;
                magazine_item.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(handler.CurrentGrabHand.m_controller) * 2;

                onDropMagazine?.Invoke();
            }
        }

        public void Restore()
        {
            if (!loaded)
            {
                loaded = true;
                magazine_item.isKinematic = true;
                magazine_item.transform.SetParent(this.transform);
                magazine_item.transform.localPosition = originPos;
                magazine_item.transform.localRotation = originRot;
            }
        }

        public override void HandleGlobalInputs()
        {
            if (handler.CurrentGrabHand != null)
            {
                if (OVRInput.GetDown(active_button, handler.CurrentGrabHand.m_controller))
                {
                    DropMagazine();
                }
            }
        }
    }
}