using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public class Luger : SimpleWeapon
    {
        [Header("Luger Parameters")]
        public Animator anim;
        public Magazine magazine_slot;
        public Slapper slapper_magazine;
        public Slapper slapper_receiver;

        private readonly float force = 10;

        protected override void OnInitWeapon()
        {
            base.OnInitWeapon();

            slapper_magazine.gameObject.SetActive(false);
            slapper_magazine.gameObject.SetActive(false);

            onShoot += AnimateReceiver;
            magazine_slot.onDropMagazine += DropMagazine;
            slapper_magazine.onSlap += PlugMagazine;
            slapper_receiver.onSlap += ReceiverRestore;
        }

        public void ReceiverRestore()
        {
            Reload();
            slapper_receiver.gameObject.SetActive(false);
            anim.SetInteger("Ammo", ammo);
        }

        private void PlugMagazine()
        {
            magazine_slot.Restore();
            slapper_magazine.gameObject.SetActive(false);
            slapper_receiver.gameObject.SetActive(true);
        }

        private void DropMagazine()
        {
            if (ammo > 0)
                ammo = 1;
            else
                ammo = 0;

            slapper_magazine.gameObject.SetActive(true);
        }

        private void AnimateReceiver()
        {
            anim.SetInteger("Ammo", ammo);
            anim.SetTrigger("Shoot");
        }
    }
}