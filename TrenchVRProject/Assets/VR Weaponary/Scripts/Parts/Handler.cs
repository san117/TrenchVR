using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public class Handler : GrabPoint
    {
        [Header("Handler Parameters")]
        public Vector3 posOffset;
        public Vector3 rotationOffset;

        public override void UpdatePart()
        {
            if (CurrentGrabHand != null)
            {
                AttachedWeapon.transform.position = CurrentGrabHand.transform.position + posOffset;
                AttachedWeapon.transform.rotation = CurrentGrabHand.transform.rotation;

                AttachedWeapon.transform.localRotation = Quaternion.Euler(rotationOffset + AttachedWeapon.transform.localRotation.eulerAngles);
            }
        }
    }

}