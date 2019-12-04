using UnityEngine;
using System.Collections;
using WeaponSystem;

namespace EnemySystem
{
    public class Enemy : MonoBehaviour
    {
        private InvasionManager manager;
        public Animator anim;

        public GameObject bullet;
        public Transform weaponBone;

        [Range(1,100)]
        public float life = 100;

        [SerializeField]
        private WayPoint current;
        private WayPoint last;

        [Range(0,100)]
        public float fear = 10;

        [Range(0, 10)]
        public int ammo = 3;

        Vector3 lastFixedPoint;
        Vector3 lastPosition;
        private float velocity;
        bool needFixPosition;

        public bool startInit = false;

        public Rigidbody weapon;

        private Transform target;

        private bool isAiming;

        private bool performAttack;

        [Header("IK")]
        public float aimRotationOffset;
        public Vector3 aimOffset;

        private Transform chest;

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            chest = anim.GetBoneTransform(HumanBodyBones.Chest);

            if (startInit)
                Init();
        }

        public void Init()
        {
            manager = FindObjectOfType<InvasionManager>();

            anim.SetFloat("Speed", Random.Range(0.7f, 1.4f));

            if (current == null)
            {
                current = manager.GetClosestWaypoint(transform.position);
                current.isOccupied = true;
            }

            anim.SetBool("Waiting", false);
        }

        private void FixedUpdate()
        {
            if (needFixPosition)
            {
                transform.position = lastFixedPoint;
                needFixPosition = false;
            }
        }

        private void LateUpdate()
        {
            if (isAiming)
            {
                chest.LookAt(target.position);
                chest.rotation = chest.rotation * Quaternion.Euler(aimOffset);
            }
        }

        private void Update()
        {
            anim.SetFloat("Fear", fear);
            anim.SetFloat("Velocity", velocity);
            anim.SetInteger("Ammo", ammo);
            anim.SetBool("HaveObjective", current != null);

            if (current != null)
            {
                transform.LookAt(current.transform, Vector3.up);
                anim.SetFloat("ObjectiveDistance", Vector3.Distance(transform.position, current.transform.position));
            }

            if (isAiming)
            {
                Quaternion rot = Quaternion.LookRotation(new Vector3(target.position.x,transform.position.y, target.position.z) - transform.position, Vector3.up);
                var rotEuler = rot.eulerAngles;
                rotEuler.y += aimRotationOffset;
                rot = Quaternion.Euler(rotEuler);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 2);
            }

            if (performAttack)
            {
                GameObject _bullet = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation);
                _bullet.SetActive(true);

                performAttack = false;
            }

            velocity = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
            lastPosition = transform.position;
        }

        public void FixPos()
        {
            needFixPosition = true;
        }

        public void FaceObjective() {

            isAiming = true;
        }

        public void Moving()
        {
            isAiming = false;
            anim.SetBool("Moving", true);
            anim.SetBool("Knee", false);
        }

        public void Attack()
        {
            ammo--;
            anim.SetBool("Moving", false);
            anim.SetBool("Knee", false);

            performAttack = true;
        }

        public void ReachWaypoint()
        {
            if (current != null)
            {
                anim.SetBool("OnCover", current.isProtected);

                if (last != null)
                    last.isOccupied = false;

                last = current;
                current = null;

                lastFixedPoint = last.transform.position;
            }
        }

        public void Kill()
        {
            if (current != null)
                current.isOccupied = false;

            if (last != null)
                last.isOccupied = false;

            this.enabled = false;
        }

        public void Damage(float damage)
        {
            life -= damage;

            if (life <= 0)
            {
                anim.enabled = false;

                weapon.transform.SetParent(null);
                weapon.isKinematic = false;
                Kill();
            }
            else
            {
                anim.SetTrigger("Damage");
            }
        }

        public void Damage(float damage, BodyPart bodypart, RaycastHit hitinfo, Vector3 force)
        {
            life -= damage;

            if (life <= 0)
            {
                anim.enabled = false;

                weapon.transform.SetParent(null);
                weapon.isKinematic = false;
                bodypart.m_collider.attachedRigidbody.AddForceAtPosition(force, hitinfo.point, ForceMode.Force);
                Kill();
            }
            else
            {
                anim.SetTrigger("Damage");
            }
        }

        public void Plan()
        {
            if (last != null)
            {
                var nextPoint = manager.GetClosestWaypoint(last);

                if (last != nextPoint)
                {
                    nextPoint.isOccupied = true;
                    current = nextPoint;
                }
            }
        }

        public void Reload()
        {
            ammo += 3;
        }

        public void Aim()
        {

        }

        public void Knee()
        {
            anim.SetBool("Moving", false);
            anim.SetBool("Knee", true);
        }
    }
}