using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public interface IShootable
    {
        void Hit(RaycastHit hit, Bullet bullet);
    }
}