using OVRTouchSample;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VRInteractiveSystem
{
    [RequireComponent(typeof(VRHandAreaController), typeof(VRInputArea))]
    public class VRButtonBehaviour : MonoBehaviour
    {
        public Animator animator;

        [SerializeField] private UnityEvent onClick;

        private VRHandAreaController m_areaController;
        private VRInputArea m_inputArea;

        private Dictionary<Hand, HandState> handState = new Dictionary<Hand, HandState>();

        public UnityAction<Hand> onClickAction;

        private enum HandState
        {
            PRESSED, RELEASED
        }

        private void Start()
        {
            m_areaController = GetComponent<VRHandAreaController>();
            m_inputArea = GetComponent<VRInputArea>();

            m_inputArea.onPressDownAction += Down;
            m_inputArea.onPressUpAction += Up;
        }

        private void Update()
        {
            animator.SetInteger("HandCount", m_areaController.handsInside.Count);
        }

        public void Down(Hand hand)
        {
            handState.Add(hand, HandState.PRESSED);
        }

        public void Up(Hand hand)
        {
            if (handState.ContainsKey(hand))
            {
                if (handState[hand] == HandState.PRESSED)
                {
                    handState[hand] = HandState.RELEASED;
                    handState.Remove(hand);


                    if (onClickAction != null)
                    {
                        onClickAction.Invoke(hand);
                    }

                    onClick.Invoke();

                    animator.SetTrigger("OnClick");
                }
            }
        }
    }
}