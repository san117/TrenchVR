using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletShell : MonoBehaviour
    {
        public float lifeTime = 5;

        public Vector3 force;

        public AudioSource source;
        public AudioClip spawn_audio;

        void Start()
        {
            GetComponent<Rigidbody>().AddRelativeForce(force, ForceMode.VelocityChange);
            GetComponent<Rigidbody>().AddRelativeTorque(Random.Range(-150f,150f), Random.Range(-150f, 150f), Random.Range(-150f, 150f));
            StartCoroutine(Destruct());

            source.PlayOneShot(spawn_audio);
        }

        IEnumerator Destruct()
        {
            yield return new WaitForSeconds(lifeTime);

            Destroy(this.gameObject);
        }

    }

}