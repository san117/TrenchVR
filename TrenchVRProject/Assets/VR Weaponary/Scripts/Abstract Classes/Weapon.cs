using OVRTouchSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Weapon : MonoBehaviour
    {
        public Transform trackingSpace;
        private Rigidbody m_rigidbody;
        private WeaponPart[] parts;
        private Hand[] hands;

        private readonly float interactiveDistance = 0.1f;

        Dictionary<Hand, WeaponPart> closestsParts = new Dictionary<Hand, WeaponPart>();

        private Vector3 originPos;
        private Quaternion originRot;

        private void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();

            parts = GetComponentsInChildren<WeaponPart>(true);
            hands = FindObjectsOfType<Hand>();

            originPos = transform.position;
            originRot = transform.rotation;

            OnInitWeapon();
        }

        public void ActivePhysics(Hand hand, float force, float angularForce)
        {
            transform.SetParent(null);
            m_rigidbody.isKinematic = false;
            m_rigidbody.velocity = trackingSpace.rotation * OVRInput.GetLocalControllerVelocity(hand.m_controller) * force;
            m_rigidbody.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(hand.m_controller) * angularForce;
        }

        public void DisctivePhysics(Hand parent)
        {
            m_rigidbody.isKinematic = true;

            transform.SetParent(parent.transform);
        }

        public void DisctivePhysics()
        {
            m_rigidbody.isKinematic = true;
        }

        private void Update()
        {
            UpdateWeapon();
            UpdateParts();

            if (Vector3.Distance(transform.position, Camera.main.transform.position) > 35)
            {
                DisctivePhysics();
                transform.position = originPos;
                transform.rotation = originRot;
            }
        }

        private void UpdateParts()
        {
            foreach (var pair in closestsParts)
            {
                pair.Value.OverHand(pair.Key);
            }

            foreach (var part in parts)
            {
                if (part.isInteractive)
                {
                    foreach (var hand in hands)
                    {
                        float distance = part.DistanceToHand(hand);

                        if (distance <= interactiveDistance)
                        {
                            if (closestsParts.TryGetValue(hand, out WeaponPart p))
                            {
                                if (part.DistanceToHand(hand) > distance)
                                {
                                    p.Unselect();
                                    closestsParts[hand] = part;
                                    part.Select(hand);
                                }
                            }
                            else
                            {
                                closestsParts.Add(hand, part);
                                part.Select(hand);
                            }
                        }

                        if (closestsParts.TryGetValue(hand, out var cachePart))
                        {
                            if (cachePart.DistanceToHand(hand) > interactiveDistance)
                            {
                                closestsParts.Remove(hand);

                                if (!closestsParts.ContainsValue(cachePart))
                                {
                                    cachePart.Unselect();
                                }
                            }
                        }
                    }
                }

                part.HandleGlobalInputs();
                part.UpdatePart();
            }
        }

        /// <summary>
        /// Called after start.
        /// </summary>
        protected virtual void OnInitWeapon() { }

        /// <summary>
        /// Called each frame.
        /// </summary>
        protected virtual void UpdateWeapon() { }
    }
}