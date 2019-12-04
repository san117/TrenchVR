using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class Bullet : MonoBehaviour
    {
        public Rigidbody rb;

        public float force = 2.5f;
        public float lifeTime = 5;
        public float impactForce = 1500;
        public float damage = 80;

        public Transform front_Bullet;
        public Transform middle_Bullet;
        public Transform back_Bullet;

        public ParticleSystem impact_particle;

        public LayerMask canImpactWith;

        void Start()
        {
            rb.AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);

            StartCoroutine(SelfDestruct());
        }

        private void Update()
        {

            if (Physics.Linecast(front_Bullet.position, middle_Bullet.position, out RaycastHit hit, canImpactWith))
            {
                if (hit.collider.attachedRigidbody != null)
                {
                    hit.collider.attachedRigidbody.AddForceAtPosition(rb.mass * rb.velocity * impactForce, hit.point, ForceMode.Force);
                }

                if (Physics.Raycast(new Ray(back_Bullet.position, back_Bullet.forward), out RaycastHit impactPoint))
                {
                    if (hit.collider.GetComponent<IShootable>() != null)
                    {
                        hit.collider.GetComponent<IShootable>().Hit(impactPoint, this);
                    }
                    else
                    {
                        GameObject particle = Instantiate(impact_particle.gameObject, impactPoint.point, Quaternion.identity);

                        particle.transform.LookAt(transform.position + impactPoint.normal);
                    }

                    Destroy(this.gameObject);
                }
                else
                {
                    GameObject particle = Instantiate(impact_particle.gameObject, hit.point, Quaternion.identity);

                    particle.transform.LookAt(transform.position + hit.normal);

                    Destroy(this.gameObject);
                }
            }
        }

        IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(lifeTime);

            Destroy(this.gameObject);
        }
    }

}