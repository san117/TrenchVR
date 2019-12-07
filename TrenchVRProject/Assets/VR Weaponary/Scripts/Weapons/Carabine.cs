using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public class Carabine : SimpleWeapon
    {
        [Header("Carabine")]
        public Rigidbody drop_bullet_prefab;
        public Transform drop_bullet_point;

        private float force = 10;

        protected override void OnInitWeapon()
        {
            base.OnInitWeapon();

            onShoot += DropBullet;
        }

        private void DropBullet()
        {
            Rigidbody db = Instantiate(drop_bullet_prefab.gameObject, drop_bullet_point).GetComponent<Rigidbody>();

            db.gameObject.SetActive(true);
            db.transform.SetParent(null);
            db.AddRelativeForce(new Vector3(Random.Range(1f, 3f), Random.Range(1f, 3f), Random.Range(-0.5f,0.5f)) * force);
            db.AddRelativeTorque(new Vector3(Random.Range(1f, 3f), Random.Range(1f, 3f), Random.Range(-0.5f, 0.5f)));
        }
    }
}