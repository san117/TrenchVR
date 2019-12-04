using OVRTouchSample;
using UnityEngine;
using UnityEngine.Events;

namespace VRInteractiveSystem
{
    public class VRInputArea : MonoBehaviour
    {
        public OVRInput.Button detect_button = OVRInput.Button.PrimaryHandTrigger;

        [SerializeField] private UnityEvent onPressDownButton;
        [SerializeField] private UnityEvent onPressingButton;
        [SerializeField] private UnityEvent onPressUpButton;

        public UnityAction<Hand> onPressDownAction;
        public UnityAction<Hand> onPressingAction;
        public UnityAction<Hand> onPressUpAction;

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
                if (OVRInput.GetDown(detect_button, hand.m_controller))
                {
                    onPressDownButton.Invoke();

                    if (onPressDownAction != null)
                    {
                        onPressDownAction.Invoke(hand);
                    }
                }

                if (OVRInput.Get(detect_button, hand.m_controller))
                {
                    onPressingButton.Invoke();

                    if (onPressingAction != null)
                    {
                        onPressingAction.Invoke(hand);
                    }
                }

                if (OVRInput.GetUp(detect_button, hand.m_controller))
                {
                    onPressUpButton.Invoke();

                    if (onPressUpAction != null)
                    {
                        onPressUpAction.Invoke(hand);
                    }
                }
            }
        }
    }
}