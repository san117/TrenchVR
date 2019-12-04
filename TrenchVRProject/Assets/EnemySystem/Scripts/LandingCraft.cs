using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingCraft : MonoBehaviour
{
    public Transform target;
    public float speed = 3.4f;

    public Animator anim;

    private bool reachCompleted = false;

    public EnemySystem.Enemy[] soldiers;

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < 0.5f && !reachCompleted)
        {
            anim.SetBool("Opening", true);
            reachCompleted = true;
        }

        if (!reachCompleted)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        }
    }

    public void InitSoldiers()
    {
        foreach (var soldier in soldiers)
        {
            soldier.transform.SetParent(null);
            soldier.Init();
        }
    }
}
