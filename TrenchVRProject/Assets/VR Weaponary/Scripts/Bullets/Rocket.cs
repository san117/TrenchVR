using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public class Rocket : MonoBehaviour
    {

        [Header("Rocket Properties")]
        public float force;
        public float damage;

        public LayerMask canImpactWith;
        RaycastHit hitPoint;

        public Vector3 Velocity;

        private Vector3 lastpos;
            
        void Start()
        {
            CalculateImpactPoint();
            StartCoroutine(Die());
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
            if (hitPoint.collider != null)
            {
                Explode();
            }
        }

        private void Explode()
        {
            Collider[] colls = Physics.OverlapSphere(transform.position, 3);
            foreach (Collider hit in colls)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(force, transform.position, 3, 1.0F);
            }

            Destroy(this.gameObject);
        }

        IEnumerator Die()
        {
            yield return new WaitForSeconds(5);
            Hit();
        }


        ///<summary>
        ///Similar a bullet. Explota al impactar
        ///</summary>
    }
}