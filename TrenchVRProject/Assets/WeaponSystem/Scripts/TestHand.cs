using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TestHand : MonoBehaviour
{
    SkinnedMeshRenderer ren;

    public UnityEvent onPressed;

    private void Start()
    {
        ren = GetComponentInChildren<SkinnedMeshRenderer>();
        ren.enabled = true;
    }

    [ContextMenu("Test")]
    public void Test()
    {
        onPressed.Invoke();
    }
}
