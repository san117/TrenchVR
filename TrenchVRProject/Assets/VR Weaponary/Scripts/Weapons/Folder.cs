using OVRTouchSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeaponary
{
    public class Folder : Weapon
    {
        public List<FolderOption> options = new List<FolderOption>();

        public float overDistance = 0.1f;
        public float activationDistance = 0.01f;

        private Hand[] hands;

        private Dictionary<Hand, FolderOption> closestOptions = new Dictionary<Hand, FolderOption>();

        protected override void OnInitWeapon()
        {
            hands = FindObjectsOfType<Hand>();
        }

        protected override void UpdateWeapon()
        {
            foreach (var pair in closestOptions)
            {
                if (Vector3.Distance(pair.Key.transform.position, pair.Value.transform.position) <= activationDistance)
                {
                    pair.Value.Select();
                }
            }

            foreach (var option in options)
            {
                foreach (var hand in hands)
                {
                    float distance = Vector3.Distance(hand.transform.position, option.transform.position);

                    if (distance <= overDistance)
                    {
                        if (closestOptions.TryGetValue(hand, out FolderOption p))
                        {
                            if (Vector3.Distance(hand.transform.position, option.transform.position) > distance)
                            {
                                p.Hide();
                                closestOptions[hand] = option;
                                option.Show();
                            }
                        }
                        else
                        {
                            closestOptions.Add(hand, option);
                            option.Show();
                        }
                    }

                    if (closestOptions.TryGetValue(hand, out var cachePart))
                    {
                        if (Vector3.Distance(hand.transform.position, cachePart.transform.position) > overDistance)
                        {
                            closestOptions.Remove(hand);

                            if (!closestOptions.ContainsValue(cachePart))
                            {
                                cachePart.Hide();
                            }
                        }
                    }
                }
            }
        }
    }
}