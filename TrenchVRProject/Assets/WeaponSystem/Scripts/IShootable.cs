using UnityEngine;
using System.Collections;

namespace WeaponSystem
{
    public interface IShootable
    {
        void Hit(RaycastHit hitinfo, Bullet bullet);
    }
}