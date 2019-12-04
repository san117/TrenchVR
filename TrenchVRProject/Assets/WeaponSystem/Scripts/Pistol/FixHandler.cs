using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using UnityEngine;
using UnityEngine.Events;

namespace WeaponSystem
{
    public class FixHandler : GrabPoint
    {
        public Vector3 rotationOffset;
        private Vector3 rOffset;

        public UnityEvent onTake;
        public UnityEvent onRelease;

        protected override void Update()
        {
            base.Update();

            if (currentGrabHand != null)
            {
                manager.transform.position = currentGrabHand.transform.position;
                manager.transform.rotation = currentGrabHand.transform.rotation;

                manager.transform.localRotation = Quaternion.Euler(rOffset + manager.transform.localRotation.eulerAngles);
            }
        }

        public override void PressedPart(Hand hand)
        {
            if (currentGrabHand == null)
            {
                hand_pose.SetActive(true);
                currentGrabHand = hand;

                if (hand.m_controller == OVRInput.Controller.LTouch)
                {
                    manager.transform.localScale = new Vector3(-1,1,1);
                    rOffset = -rotationOffset;
                }
                else if(hand.m_controller == OVRInput.Controller.RTouch)
                {
                    manager.transform.localScale = new Vector3(1, 1, 1);
                    rOffset = rotationOffset;
                }

                manager.DisctivePhysics();
                currentGrabHand.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                onTake.Invoke();
            }
        }

        public override void UnpressedPart()
        {
            hand_pose.SetActive(false);
            manager.ActivePhysics(currentGrabHand, 2, 2);
            currentGrabHand.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            currentGrabHand = null;
            onRelease.Invoke();
        }
    }
}