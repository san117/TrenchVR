using OVRTouchSample;
using UnityEngine;
using System;

namespace VRWeaponary
{
    [RequireComponent(typeof(Outline))]
    public abstract class WeaponPart : MonoBehaviour
    {
        [Header("VR Active Button")]
        public OVRInput.Button active_button = OVRInput.Button.PrimaryHandTrigger;
        public bool isInteractive;

        public Weapon AttachedWeapon { get; private set; }
        public bool IsSelected { get; private set; }

        private Hand insideHand;

        //Events
        public Action<Hand> onHandEnter;

        private void Start()
        {
            FindWeapon(transform);
            InitPart();
            Unselect();
        }

        private void FindWeapon(Transform current)
        {
            AttachedWeapon = current.GetComponent<Weapon>();

            if (AttachedWeapon == null)
            {
                if (transform.parent != null)
                {
                    FindWeapon(current.parent);
                }
                else
                {
                    return;
                }
            }else
            {
                return;
            }
        }

        public virtual void HandleGlobalInputs() { }

        /// <summary>
        /// Highlight renderer.
        /// </summary>
        /// <param name="hand"></param>
        public void Select(Hand hand)
        {
            IsSelected = true;
            insideHand = hand;
            gameObject.GetComponent<Outline>().OutlineWidth = 10;
        }

        public void OverHand(Hand hand)
        {
            HanldeLocalInputs(hand);

            onHandEnter?.Invoke(hand);
        }

        /// <summary>
        /// Unhighlight renderer.
        /// </summary>
        public void Unselect()
        {
            IsSelected = false;
            insideHand = null;
            gameObject.GetComponent<Outline>().OutlineWidth = 1;
        }

        private void HanldeLocalInputs(Hand hand)
        {
            if (OVRInput.Get(active_button, hand.m_controller))
            {
                LocalGetButton(hand);
            }

            if (OVRInput.GetDown(active_button, hand.m_controller))
            {
                LocalGetDownButton(hand);
            }

            if (OVRInput.GetUp(active_button, hand.m_controller))
            {
                LocalGetUpButton(hand);
            }
        }

        public virtual void InitPart() { }
        public virtual void UpdatePart() { }

        /// <summary>
        /// Called when OVR masked button is pressed while is beign selected;
        /// </summary>
        /// <param name="hand"></param>
        public virtual void LocalGetButton(Hand hand) { }
        /// <summary>
        /// Called when OVR masked button was pressed in this frame while is beign selected;
        /// </summary>
        /// <param name="hand"></param>
        public virtual void LocalGetDownButton(Hand hand) { }
        /// <summary>
        /// Called when OVR masked button was pressed in this frame while is beign selected;
        /// </summary>
        /// <param name="hand"></param>
        public virtual void LocalGetUpButton(Hand hand) { }
    }
}