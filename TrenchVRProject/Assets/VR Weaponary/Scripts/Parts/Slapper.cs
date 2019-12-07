using OVRTouchSample;
using System;

namespace VRWeaponary
{
    public class Slapper : WeaponPart
    {
        public float threshold = 1.5f;

        public Action onSlap;

        public override void InitPart()
        {
            onHandEnter += EnterHand;
        }

        private void EnterHand(Hand hand)
        {
            if (hand.velocity.magnitude >= threshold)
            {
                onSlap?.Invoke();
            }
        }
    }
}