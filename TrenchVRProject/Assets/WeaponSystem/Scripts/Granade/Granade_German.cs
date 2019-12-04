using OVRTouchSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem.Weapons
{
    public class Granade_German : WeaponManager
    {
        public Animator anim;
        public ParticleSystem exploteParticle;
        public float trhowForce = 1.5f;

        public void StartCountDown()
        {
            StartCoroutine(CountDown());
        }

        private IEnumerator CountDown()
        {
            int timer = 4;
            while(timer > 0)
            {
                float delta = Time.deltaTime;

                yield return new WaitForSeconds(1 - delta);
                timer--;

                if (mainHand != null)
                    OVRInput.SetControllerVibration(0.1f, 0.4f, mainHand.m_controller);

                yield return new WaitForSeconds(delta);

                if (mainHand != null)
                    OVRInput.SetControllerVibration(0, 0, mainHand.m_controller);
            }

            Shoot();
        }

        public override void OnReleaseHand(Hand hand)
        {
            if (hand.Equals(mainHand))
            {
                mainHand = null;
                transform.SetParent(null);
                ActivePhysics(hand, trhowForce, 3);
            }
        }

        [ContextMenu("Explote")]
        public override void Shoot()
        {
            Instantiate(exploteParticle.gameObject, transform.position, Quaternion.identity);
            source.Play();

            m_rigidbody.constraints = RigidbodyConstraints.FreezePosition;
            anim.SetTrigger("Explote");

            StartCoroutine(SimulateExplosionFeel());
        }

        IEnumerator SimulateExplosionFeel()
        {
            float amplitude = 1.5f;

            while (amplitude > 0)
            {
                OVRInput.SetControllerVibration(0.01f, amplitude, OVRInput.Controller.All);

                amplitude -= Time.deltaTime * 3;

                amplitude = Mathf.Clamp(amplitude, 0, 1.5f);

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        public void Dispose()
        {
            Destroy(this.gameObject);
        }
    }

}