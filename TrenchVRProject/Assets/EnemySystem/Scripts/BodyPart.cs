using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem;

namespace EnemySystem
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class BodyPart : MonoBehaviour, IShootable
    {
        public LayerMask collidableObjects;

        public Enemy enemy;

        public ParticleSystem impact_particle;
        [HideInInspector] public Collider m_collider;

        public float damageMultiply = 1;

        private void Start()
        {
            m_collider = GetComponent<Collider>();
        }

        public void Hit(RaycastHit hitinfo, Bullet bullet)
        {
          //  if (Random.Range(0f,1f) <= impactProbability)
          //  {
                GameObject particle = Instantiate(impact_particle.gameObject, hitinfo.point, Quaternion.identity);

                particle.transform.LookAt(transform.position + hitinfo.normal);

                enemy.Damage(bullet.damage * damageMultiply, this, hitinfo, bullet.rb.mass * bullet.rb.velocity * bullet.impactForce);
          //  }
        }
    }

}