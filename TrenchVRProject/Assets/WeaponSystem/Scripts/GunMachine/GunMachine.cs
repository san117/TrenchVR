using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WeaponSystem.Weapons
{
    public class GunMachine : WeaponManager
    {
        public GrabPoint left;
        public GrabPoint right;
        public Transform pivot;
        public GameObject bullet;
        public Animator anim;

        public float turnspeed = 2.5f;
        public float shootSpeed = 0.05f;

        public bool autoshoot;

        private float nextShoot;
        private bool stopShoot = false;
        private bool shooting = false;

        public UnityEvent onStartShoot;
        public UnityEvent onShoot;
        public UnityEvent onStopShoot;

        public void Update()
        {
            if (left.currentGrabHand != null && right.currentGrabHand != null)
            {
                Vector3 middlePoint = (left.currentGrabHand.transform.position + right.currentGrabHand.transform.position) / 2;

                Quaternion targetRotation = Quaternion.LookRotation(-(middlePoint - pivot.position));

                pivot.transform.rotation = Quaternion.Slerp(pivot.transform.rotation, targetRotation, turnspeed * Time.deltaTime);

                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, left.currentGrabHand.m_controller) && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, right.currentGrabHand.m_controller))
                {
                    Shoot();
                }else 
                {
                    if (shooting)
                    {
                        stopShoot = true;
                    }
                }
            }

            if (autoshoot)
            {
                Shoot();
            }

            if ((left.currentGrabHand == null || right.currentGrabHand == null) && shooting)
            {
                stopShoot = true;
            }

            if (stopShoot)
            {
                stopShoot = false;
                shooting = false;

                onStopShoot.Invoke();

                if (OVRInput.IsControllerConnected(left.currentGrabHand.m_controller))
                {
                    OVRInput.SetControllerVibration(0, 0, left.currentGrabHand.m_controller);
                }

                if (OVRInput.IsControllerConnected(right.currentGrabHand.m_controller))
                {
                    OVRInput.SetControllerVibration(0, 0, right.currentGrabHand.m_controller);
                }
            }

            anim.SetBool("Shooting", shooting);
        }

        [ContextMenu("Shoot")]
        public override void Shoot()
        {
            if (Time.time > nextShoot && ammo > 0)
            {
                if (!shooting)
                {
                    shooting = true;
                    onStartShoot.Invoke();
                }

                nextShoot = Time.time + shootSpeed;

                ammo--;

                GameObject _bullet = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation);
                _bullet.SetActive(true);

                onShoot.Invoke();

                if (ammo <= 0)
                {
                    stopShoot = true;
                }

                if (OVRInput.IsControllerConnected(left.currentGrabHand.m_controller))
                {
                    OVRInput.SetControllerVibration(0.8f, 0.3f, left.currentGrabHand.m_controller);
                }

                if (OVRInput.IsControllerConnected(right.currentGrabHand.m_controller))
                {
                    OVRInput.SetControllerVibration(0.8f, 0.3f, right.currentGrabHand.m_controller);
                }
            }
        }
    }
}