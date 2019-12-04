using OVRTouchSample;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VRInteractiveSystem
{
    [RequireComponent(typeof(VRHandAreaDetector))]
    public class VRHandAreaController : MonoBehaviour
    {
        private VRHandAreaDetector detector;

        public List<Hand> handsInside = new List<Hand>();

        private void Start()
        {
            detector = GetComponent<VRHandAreaDetector>();

            detector.onHandEnterAction += RegisterHand;
            detector.onHandExitAction += UnregisterHand;
        }

        private void RegisterHand(Hand hand)
        {
            if (!handsInside.Contains(hand))
            {
                handsInside.Add(hand);
            }
        }

        private void UnregisterHand(Hand hand)
        {
            if (handsInside.Contains(hand))
            {
                handsInside.Remove(hand);
            }
        }
    }
}