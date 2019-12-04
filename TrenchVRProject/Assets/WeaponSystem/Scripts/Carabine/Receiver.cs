using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using UnityEngine;

namespace WeaponSystem
{
    public class Receiver : WeaponPart
    {
        public ConfigurableJoint joint;
        public Transform jointPoint;

        public Transform openPoint;
        public Transform closePoint;

        [SerializeField]
        private GameObject fakeBullet_template;
        [SerializeField]
        private Transform fakeBullet_spawnPos;

        private bool isOpen = false;

        [SerializeField]
        private Hand current;

        [Header("Audios")]
        public AudioClip openReceiver_audio;
        public AudioClip closeReceiver_audio;

        private void Start()
        {
            fakeBullet_template.SetActive(false);
        }

        protected override void Update()
        {
            base.Update();

            if (current != null)
            {
                if (OVRInput.GetUp(detect_button, current.m_controller))
                {
                    current = null;
                }
            }
        }

        private void FixedUpdate()
        {
            if (current != null)
            {
                var moveDelta = current.transform.position.z - jointPoint.position.z;
                var vector = transform.position;
                vector.z += moveDelta;

                transform.position = vector;

                if (isOpen && Vector3.Distance(jointPoint.position, closePoint.position) <= 0.03f)
                {
                    Close();
                    return;
                }

                if (!isOpen && Vector3.Distance(jointPoint.position, openPoint.position) <= 0.03f)
                {
                    Open();
                    return;
                }
            }
        }

        [ContextMenu("Retract")]
        public void Retract()
        {
            current = null;
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * 3.5f, ForceMode.VelocityChange);

            Instantiate(fakeBullet_template, fakeBullet_spawnPos.position, Quaternion.identity).SetActive(true);
        }

        private void Open()
        {
            isOpen = true;
            manager.source.PlayOneShot(openReceiver_audio);
            manager.ammo = 3;
        }

        private void Close()
        {
            isOpen = false;
            manager.source.PlayOneShot(closeReceiver_audio);
        }

        public override void OnHandEnter(Hand hand)
        {
            
        }

        public override void OnHandExit(Hand hand)
        {
           
        }

        public override void PressedPart(Hand hand)
        {
            var drive = joint.zDrive;
            drive.maximumForce = 0.015f;

            joint.zDrive = drive;
            joint.zMotion = ConfigurableJointMotion.Limited;

            current = hand;
        }

        public override void UnpressingPart(Hand hand)
        {

        }

        public override void TriggeringPart(float currentValue, Hand hand)
        {
        }

        public override void PressingPart(Hand hand)
        {
            
        }
    }
}