using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using UnityEngine.Events;

namespace WeaponSystem
{
    public class PressurePlate : WeaponPart
    {
        public float threshold = 1.5f;
        public UnityEvent onPush;

        public override void OnHandEnter(Hand hand)
        {
            if (hand.velocity.magnitude >= threshold)
            {
                onPush.Invoke();
            }
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