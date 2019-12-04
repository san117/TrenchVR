using OVRTouchSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Weapon : MonoBehaviour
    {
        private Rigidbody m_rigidbody;
        private WeaponPart[] parts;
        private Hand[] hands;

        private float interactiveDistance = 0.5f;

        private void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();

            parts = GetComponentsInChildren<WeaponPart>(true);
            hands = FindObjectsOfType<Hand>();
        }

        private void Update()
        {
            UpdateParts();   
        }

        private void UpdateParts()
        {
            foreach (var part in parts)
            {
                part.Unselect();

                foreach (var hand in hands)
                {

                }

                part.UpdatePart();
            }
        }
    }
}