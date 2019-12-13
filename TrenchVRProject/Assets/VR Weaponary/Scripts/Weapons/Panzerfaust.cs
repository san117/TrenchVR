using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public class Panzerfaust : Weapon
    {
        [Header("Panzerfaust Parts")]
        public GrabPoint grabPoint;
        public Trigger trigger;
        public RocketPart rocketPart;

        [Header("Panzerfaust Components")]
        public Transform weapon_body;

        [Header("Panzerfaust Parameters")]
        public float turnspeed = 1;

        [Header("Audios")]
        public AudioClip shoot_audio;
        public AudioClip shoot_noammo_audio;
        public AudioClip reload_audio;

        //Variables
        private bool shooting = false;
        private bool loaded = true;

        protected override void OnInitWeapon()
        {
            trigger.OnTrigger += StartShoot;
        }

        public void StartShoot()
        {
            shooting = true;
        }

        public void Shoot()
        {
            if (grabPoint.CurrentGrabHand != null && trigger.grabPoint.CurrentGrabHand != null)
            {
                loaded = false;
                rocketPart.Shoot();
            }
            else
            {
                shooting = false; //asegurarse de que no dispare
            }
        }


        protected override void UpdateWeapon()
        {
            if (shooting && loaded)
            {
                Shoot();
            }

            if (!(shooting || loaded))
            {
                if (!(grabPoint.CurrentGrabHand != null || trigger.grabPoint.CurrentGrabHand != null))
                {
                    Destroy(grabPoint);
                    Destroy(trigger);
                }
            }
        }
    }
}
