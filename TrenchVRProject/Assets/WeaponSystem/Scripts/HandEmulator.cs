using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class HandEmulator : MonoBehaviour
{
    SkinnedMeshRenderer ren;

    public KeyCode keycode = KeyCode.None;

    public UnityEvent onPressed;

    private void Start()
    {
        ren = GetComponentInChildren<SkinnedMeshRenderer>();
        ren.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(keycode))
        {
            onPressed.Invoke();
        }
    }
}
