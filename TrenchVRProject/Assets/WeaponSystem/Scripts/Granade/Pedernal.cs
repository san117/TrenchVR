using OVRTouchSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class Pedernal : Handler
    {
        public Handler connectedHandler;

        private LineRenderer line;

        public Transform anchor;

        float handlerVelocity;
        Vector3 lastHandlerPosition;

        private bool isActive = false;

        public AudioClip active_audio;

        private void Start()
        {
            line = GetComponent<LineRenderer>();
        }

        protected override void Update()
        {
            base.Update();

            if (GetHandler != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, GetHandler.transform.position, Time.deltaTime * 3f);
                line.SetPosition(0, transform.position);
                line.SetPosition(1, anchor.position);

                float currentDistance = Vector3.Distance(GetHandler.transform.position, anchor.position);

                Color color = Color.Lerp(Color.black, Color.red, currentDistance / 0.3f);

                line.startColor = color;
                line.endColor = color;

                if (currentDistance > 0.3f)
                {
                    Vector3 fromOriginToObject = GetHandler.transform.position - anchor.position; 
                    fromOriginToObject *= 0.3f / currentDistance;

                    transform.position = Vector3.MoveTowards(transform.position, anchor.position + fromOriginToObject, Time.deltaTime * 3f);
                    line.SetPosition(0, transform.position);

                    handlerVelocity = ((GetHandler.transform.position - lastHandlerPosition).magnitude) / Time.deltaTime;

                    if (handlerVelocity > 2)
                    {
                        isActive = true;
                    }
                }
                else
                {
                    handlerVelocity = ((GetHandler.transform.position - lastHandlerPosition).magnitude) / Time.deltaTime;
                }

                lastHandlerPosition = GetHandler.transform.position;
            }
            else
            {
                if (Vector3.Distance(transform.position, anchor.position) > 0.001f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, anchor.position, Time.deltaTime * 8f);
                }

                line.SetPosition(0, transform.position);
                line.SetPosition(1, transform.position);

                handlerVelocity = 0;
            }

            transform.LookAt(anchor.position);

            if (isActive)
            {
                gameObject.SetActive(false);

                manager.source.PlayOneShot(active_audio);

                ((Weapons.Granade_German)(manager)).StartCountDown();
            }
        }

        public override void PressedPart(Hand hand)
        {
            if (GetHandler == null)
            {
                GetHandler = hand;

                manager.source.PlayOneShot(hold_audio);
            }
        }
    }
}