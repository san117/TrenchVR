using UnityEngine;
using System.Collections;
using EnemySystem;

namespace WeaponSystem
{
    public class ExplosionDamage : MonoBehaviour
    {
        public float damage = 100;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.GetComponent<Enemy>() != null)
            {
                other.transform.root.GetComponent<Enemy>().Damage(damage);
            }
        }
    }

}