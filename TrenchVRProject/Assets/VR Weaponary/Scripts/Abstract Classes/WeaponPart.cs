using OVRTouchSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public abstract class WeaponPart : MonoBehaviour
    {
        public MeshRenderer part;

        public void Select(Hand hand)
        {

        }

        public void Unselect()
        {

        }

        public virtual void UpdatePart() { }
    }
}