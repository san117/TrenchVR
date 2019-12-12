using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Panzer : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private Renderer left_track;
    [SerializeField] private Renderer right_track;

    public float engine_power;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.AddRelativeForce(Vector3.forward * engine_power * 0.1f, ForceMode.VelocityChange);

        left_track.material.mainTextureOffset = new Vector2(0,rb.velocity.magnitude);
        right_track.material.mainTextureOffset = new Vector2(0,rb.velocity.magnitude);

        anim.SetFloat("Velocity", rb.velocity.magnitude);
    }
}
