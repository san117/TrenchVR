using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public class Bazooka : Weapon
    {

        [Header("Bazooka Parts")]
        public GrabPoint grabPoint;
        public Trigger trigger;
        public RocketPart rocketPart;


        [Header("Bazooka Parameters")]
        public bool loaded;

        [Header("Audios")]
        public AudioClip shoot_audio;
        public AudioClip shoot_noammo_audio;
        public AudioClip reload_audio;


        //Variables
        private bool shooting = false;

        protected override void OnInitWeapon()
        {
            trigger.OnTrigger += StartShoot;

            if (rocketPart != null)
            {
                loaded = true;
            }
            else
            {
                loaded = false;
            }
        }

        public void StartShoot()
        {
            shooting = true;
        }

        protected override void UpdateWeapon()
        {
            if (loaded && shooting)
            {
                Shoot();
            }
        }

        public void Shoot()
        {
            rocketPart.Shoot();
            loaded = false;
        }

        ///<summary>
        ///Similar a Panzerfaust, pero puede ser recargado 
        ///arrastrandole cohetes
        ///</summary>
    }
}
