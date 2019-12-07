using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public class Bullet : MonoBehaviour
    {
        public float force = 2.5f;
        public float lifeTime = 5;
        public float impactForce = 1500;
        public float damage = 80;
        public float weight = 0.117f;

        public ParticleSystem default_impact_particle;

        public LayerMask canImpactWith;

        public float Mass
        {
            get
            {
                return Mathf.Abs((weight / Physics.gravity.y));
            }
        }

        public Vector3 Velocity { get; private set; }

        private Vector3 lastpos;

        RaycastHit hitPoint;

        void Start()
        {
            CalculateImpactPoint();
            StartCoroutine(SelfDestruct());
        }

        private void CalculateImpactPoint()
        {
            if (!Physics.Raycast(transform.position, transform.forward, out hitPoint, canImpactWith))
            {
                hitPoint.point = transform.forward * 100000;
            }
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, hitPoint.point, Time.deltaTime * force);

            if (Vector3.Distance(transform.position, hitPoint.point) < 0.05f)
            {
                Hit();
            }

            Velocity = (transform.position - lastpos) / Time.deltaTime;

            lastpos = transform.position;
        }

        private void Hit()
        {
            if (hitPoint.collider.attachedRigidbody != null)
            {
                hitPoint.collider.attachedRigidbody.AddForceAtPosition(Mass* Velocity * impactForce, hitPoint.point, ForceMode.Force);
            }

            if (hitPoint.collider.GetComponent<IShootable>() != null)
            {
                hitPoint.collider.GetComponent<IShootable>().Hit(hitPoint, this);
            }
            else
            {
                GameObject particle = Instantiate(default_impact_particle.gameObject, hitPoint.point, Quaternion.identity);

                particle.transform.LookAt(transform.position + hitPoint.normal);
            }

            Destroy(this.gameObject);
        }

        IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(lifeTime);

            Destroy(this.gameObject);
        }
    }
}