using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WeaponSystem
{
    public class SimpleReceiver : GrabPoint
    {
        public Transform maxPoint;
        public Transform minPoint;

        public float dampForce = 3;

        public UnityEvent onMaxReach;
        public UnityEvent onMinReach;
        public UnityEvent onReleaseHand;

        private float maxDistance;

        private bool maxReach = true;
        private bool minReach;

        private bool grabbing;

        protected override void Start()
        {
            base.Start();

            maxDistance = Vector3.Distance(maxPoint.position, minPoint.position);
        }

        private void MaxPointReach()
        {
            if (!maxReach)
            {
                maxReach = true;
                minReach = false;

                onMaxReach?.Invoke();
            }
        }

        private void MinPointReach()
        {
            if (!minReach)
            {
                maxReach = false;
                minReach = true;

                onMinReach?.Invoke();
            }
        }

        protected override void Update()
        {
            base.Update();

            if (currentGrabHand == null)
            {
                transform.position = Vector3.MoveTowards(transform.position, maxPoint.position, Time.deltaTime * dampForce);

                if (grabbing)
                {
                    onReleaseHand.Invoke();
                    grabbing = false;
                }

                MaxPointReach();
            }
            else
            {
                grabbing = true;
                Vector3 nextpos = new Vector3(maxPoint.localPosition.x, maxPoint.localPosition.y, transform.parent.InverseTransformPoint(currentGrabHand.transform.position).z);

                if (nextpos.z > maxPoint.localPosition.z)
                {
                    nextpos.z = maxPoint.localPosition.z;
                    
                } else if (nextpos.z < minPoint.localPosition.z)
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
    }
}