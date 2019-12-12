using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSoldier : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim.SetFloat("Speed", Random.Range(1.1f, 1.7f));
    }
}
