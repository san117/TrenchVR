using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using UnityEngine;
using UnityEngine.Events;

namespace WeaponSystem
{
    public class SimpleTrigger : WeaponPart
    {
        public float threshold_press = 0.2f;
        public float threshold_release = 0.2f;
        private bool pressed;

        public UnityEvent onButtonPress;
        public UnityEvent onTrigger;
        public UnityEvent onUntrigger;

        public void Trigger()
        {
            if (!pressed)
            {
                pressed = true;
                onTrigger.Invoke();
            }
        }

        public void Untrigger()
        {
            if (pressed)
            {
                pressed = false;
                onUntrigger.Invoke();
            }
        }

        public override void OnHandEnter(Hand hand)
        {
           
        }

        public override void OnHandExit(Hand hand)
        {
            
        }

        public override void PressedPart(Hand hand)
        {
            onButtonPress.Invoke();
        }

        public override void PressingPart(Hand hand)
        {
            
        }

        public override void TriggeringPart(float currentValue, Hand hand)
        {
            if (currentValue > threshold_press)
            {
                Trigger();
            }
            else if (currentValue < threshold_release)
            {
                Untrigger();
            }
        }

        public override void UnpressingPart(Hand hand)
        {
           
        }
    }

}