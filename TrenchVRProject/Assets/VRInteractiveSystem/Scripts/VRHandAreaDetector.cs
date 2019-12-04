using OVRTouchSample;
using UnityEngine;
using UnityEngine.Events;

namespace VRInteractiveSystem
{
    public class VRHandAreaDetector : MonoBehaviour
    {
        [SerializeField] private UnityEvent onHandEnter;
        [SerializeField] private UnityEvent onHandStay;
        [SerializeField] private UnityEvent onHandExit;

        public UnityAction<Hand> onHandEnterAction;
        public UnityAction<Hand> onHandStayAction;
        public UnityAction<Hand> onHandExitAction;

        private void OnTriggerEnter(Collider other)
        {
            Hand hand = null;

            if (other.GetComponent<Hand>())
            {
                hand = other.GetComponent<Hand>();
            }
            else if (other.GetComponentInParent<Hand>())
            {
                hand = other.GetComponentInParent<Hand>();
            }

            if (hand != null)
            {
                onHandEnter.Invoke();

                if (onHandEnterAction != null)
                {
                    onHandEnterAction.Invoke(hand);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            Hand hand = null;

            if (other.GetComponent<Hand>())
            {
                hand = other.GetComponent<Hand>();
            }
            else if (other.GetComponentInParent<Hand>())
            {
                hand = other.GetComponentInParent<Hand>();
            }

            if (hand != null)
            {
                onHandStay.Invoke();

                if (onHandStayAction != null)
                {
                    onHandStayAction.Invoke(hand);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Hand hand = null;

            if (other.GetComponent<Hand>())
            {
                hand = other.GetComponent<Hand>();
            }
            else if (other.GetComponentInParent<Hand>())
            {
                hand = other.GetComponentInParent<Hand>();
            }

            if (hand != null)
            {
                onHandExit.Invoke();

                if (onHandExitAction != null)
                {
                    onHandExitAction.Invoke(hand);
                }
            }
        }
    }
}