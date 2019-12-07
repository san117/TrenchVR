using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class ImpactableObject : MonoBehaviour, IShootable
    {
        public LayerMask collidableObjects;

        public ParticleSystem impact_particle;
        [HideInInspector] public Collider m_collider;

        public float damageMultiply = 1;

        private void Start()
        {
            m_collider = GetComponent<Collider>();
        }

        public virtual void Hit(RaycastHit hitinfo, Bullet bullet)
        {
            GameObject particle = Instantiate(impact_particle.gameObject, hitinfo.point, Quaternion.identity);

            particle.transform.LookAt(transform.position + hitinfo.normal);
        }
    }

}