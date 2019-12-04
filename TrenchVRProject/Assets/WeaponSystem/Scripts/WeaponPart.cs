using OVRTouchSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class WeaponPart : MonoBehaviour
    {
        public WeaponManager manager;

        public OVRInput.Axis1D detect_trigger = OVRInput.Axis1D.PrimaryHandTrigger;
        public OVRInput.Button detect_button = OVRInput.Button.One;

        private List<Hand> handsInside = new List<Hand>();

        protected virtual void Update()
        {
            foreach (var hand in handsInside)
            {
                if (OVRInput.Get(detect_trigger, hand.m_controller) > 0)
                {
                    TriggeringPart(OVRInput.Get(detect_trigger, hand.m_controller), hand);
                }

                if (OVRInput.GetDown(detect_button, hand.m_controller))
                {
                    PressedPart(hand);
                }

                if (OVRInput.Get(detect_button, hand.m_controller))
                {
                    PressingPart(hand);
                }

                if (OVRInput.GetUp(detect_button, hand.m_controller))
                {
                    UnpressingPart(hand);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<Hand>())
            {
                EnterHand(other.GetComponent<Hand>());
                return;
            }

            if (other.GetComponentInParent<Hand>())
            {
                EnterHand(other.GetComponentInParent<Hand>());
                return;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Hand>())
            {
                ExitHand(other.GetComponent<Hand>());
                return;
            }

            if (other.GetComponentInParent<Hand>())
            {
                ExitHand(other.GetComponentInParent<Hand>());
                return;
            }
        }

        private void EnterHand(Hand hand)
        {
            if (!handsInside.Contains(hand))
            {
                handsInside.Add(hand);
                OnHandEnter(hand);
            }
        }

        private void ExitHand(Hand hand)
        {
            if (handsInside.Contains(hand))
            {
                handsInside.Remove(hand);
                OnHandExit(hand);
            }
        }

        public abstract void OnHandEnter(Hand hand);
        public abstract void OnHandExit(Hand hand);
        public abstract void TriggeringPart(float currentValue, Hand hand);
        public abstract void PressedPart(Hand hand);
        public abstract void PressingPart(Hand hand);
        public abstract void UnpressingPart(Hand hand);
    }

}