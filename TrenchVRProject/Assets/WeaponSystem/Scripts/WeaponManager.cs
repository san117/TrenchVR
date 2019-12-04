using OVRTouchSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class WeaponManager : MonoBehaviour
    {
        public Rigidbody m_rigidbody;
        public Transform trackingSpace;
        public AudioSource source;
        protected Hand mainHand;

        public int ammo = 3;

        [Header("Audios")]
        public AudioClip touch_audio; 

        public void ActivePhysics(Hand hand, float force, float angularForce)
        {
            m_rigidbody.isKinematic = false;
            m_rigidbody.velocity = trackingSpace.rotation * OVRInput.GetLocalControllerVelocity(hand.m_controller) * force;
            m_rigidbody.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(hand.m_controller) * angularForce;
        }

        public void DisctivePhysics()
        {
            m_rigidbody.isKinematic = true;
        }

        public virtual void Reload(int ammo)
        {
            this.ammo = ammo;
        }

        public void SetMainHand(Hand hand)
        {
            transform.SetParent(hand.transform);
            DisctivePhysics();

            mainHand = hand;
            OnSetMainHand();

            source.PlayOneShot(touch_audio);
        }

        public void ReleaseHand(Hand hand)
        {
            OnReleaseHand(hand);
        }

        public virtual void OnSetMainHand()
        {

        }

        public virtual void OnReleaseHand(Hand hand)
        {

        }

        public virtual void Shoot()
        {

        }
    }

}