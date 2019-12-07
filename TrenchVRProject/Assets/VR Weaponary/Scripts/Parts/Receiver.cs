using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace VRWeaponary
{
    public class Receiver : GrabPoint
    {
        [Header("Receiver Parameters")]
        public Transform maxPoint;
        public Transform minPoint;
        public float dampForce = 0.8f;

        //Events
        public Action onMaxReach;
        public Action onMinReach;

        //Cache variables
        private bool grabbing;
        private float maxDistance;
        private bool maxReach = true;
        private bool minReach;

        [Header("Audios")]
        public AudioClip reachMaxPoint_audio;
        public AudioClip reachMinPoint_audio;

        public override void InitPart()
        {
            base.InitPart();

            maxDistance = Vector3.Distance(maxPoint.position, minPoint.position);
        }

        public override void UpdatePart()
        {
            if (CurrentGrabHand == null)
            {
                transform.position = Vector3.MoveTowards(transform.position, maxPoint.position, Time.deltaTime * dampForce);

                if (grabbing)
                {
                    grabbing = false;
                }

                MaxPointReach();
            }
            else
            {
                grabbing = true;

                Vector3 nextpos = new Vector3(maxPoint.localPosition.x, maxPoint.localPosition.y, transform.parent.InverseTransformPoint(CurrentGrabHand.transform.position).z);

                if (nextpos.z > maxPoint.localPosition.z)
                {
                    nextpos.z = maxPoint.localPosition.z;

                }
                else if (nextpos.z < minPoint.localPosition.z)
                {
                    nextpos.z = minPoint.localPosition.z;
                }

                transform.localPosition = Vector3.MoveTowards(transform.localPosition, nextpos, Time.deltaTime * dampForce * 2);

                if (transform.localPosition.z == maxPoint.localPosition.z)
                {
                    MaxPointReach();
                }
                else if (transform.localPosition.z == minPoint.localPosition.z)
                {
                    MinPointReach();
                }
            }
        }

        private void MaxPointReach()
        {
            if (!maxReach)
            {
                maxReach = true;
                minReach = false;

                onMaxReach?.Invoke();

                AudioSource.PlayClipAtPoint(reachMaxPoint_audio, transform.position);
            }
        }

        private void MinPointReach()
        {
            if (!minReach)
            {
                maxReach = false;
                minReach = true;

                onMinReach?.Invoke();

                AudioSource.PlayClipAtPoint(reachMinPoint_audio, transform.position);
            }
        }
    }
}