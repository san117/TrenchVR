using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using UnityEngine;

namespace WeaponSystem
{
    public class Magazine : WeaponPart
    {
        public Rigidbody magazine_item;
        public FixHandler handler;
        private Vector3 originPos;
        private Quaternion originRot;

        private void Start()
        {
            originPos = magazine_item.transform.localPosition;
            originRot = magazine_item.transform.localRotation;
        }

        public void Drop()
        {
            magazine_item.isKinematic = false;
            magazine_item.transform.SetParent(null);

            magazine_item.velocity = manager.trackingSpace.rotation * OVRInput.GetLocalControllerVelocity(handler.currentGrabHand.m_controller) * 2;
            magazine_item.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(handler.currentGrabHand.m_controller) * 2;
        }

        public void Restore()
        {
            magazine_item.isKinematic = true;
            magazine_item.transform.SetParent(this.transform);
            magazine_item.transform.localPosition = originPos;
            magazine_item.transform.localRotation = originRot;
        }

        public override void OnHandEnter(Hand hand)
        {
        }

        public override void OnHandExit(Hand hand)
        {
        }

        public override void PressedPart(Hand hand)
        {
        }

        public override void PressingPart(Hand hand)
        {
        }

        public override void TriggeringPart(float currentValue, Hand hand)
        {
        }

        public override void UnpressingPart(Hand hand)
        {
        }
    }
}
