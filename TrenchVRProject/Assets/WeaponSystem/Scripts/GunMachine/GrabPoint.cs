using UnityEngine;
using System.Collections;
using OVRTouchSample;

namespace WeaponSystem
{
    public class GrabPoint : WeaponPart
    {
        public GameObject hand_pose;

        public Hand currentGrabHand;

        protected virtual void Start()
        {
            hand_pose.SetActive(false);
        }

        public override void OnHandEnter(Hand hand)
        {
           
        }

        public override void OnHandExit(Hand hand)
        {
           
        }

        protected override void Update()
        {
            base.Update();

            if (currentGrabHand != null && OVRInput.GetUp(detect_button, currentGrabHand.m_controller))
            {
                UnpressedPart();
            }
        }

        public override void PressedPart(Hand hand)
        {
            if (currentGrabHand == null)
            {
                hand_pose.SetActive(true);
                currentGrabHand = hand;
                currentGrabHand.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            }
        }

        public virtual void UnpressedPart()
        {
            hand_pose.SetActive(false);
            currentGrabHand.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            currentGrabHand = null;
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