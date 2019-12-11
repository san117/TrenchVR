using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEffect : MonoBehaviour
{
    public float nextEffectIn;

    public ParticleSystem effect;
    public AudioSource source;

    private void Start()
    {
        nextEffectIn = Time.time + Random.Range(20, 120);
    }

    private void Update()
    {
        if (Time.time >= nextEffectIn)
        {
            Effect();
        }
    }

    public void Effect()
    {
        nextEffectIn = Time.time + Random.Range(20, 120);
        effect.Play();
        source.Play();
    }
}
