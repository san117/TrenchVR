using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public class RocketPart : WeaponPart
    {

        [Header("Rocket")]
        public Rocket rocket;

        public void Shoot()
        {
            rocket.enabled = true;
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Activa un cohete y luego destruye el objeto al cual esta compuesto.
    /// Para ser usado con Panzerfaust
    /// </summary>

}