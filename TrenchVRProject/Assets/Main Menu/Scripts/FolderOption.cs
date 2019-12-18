using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class FolderOption : MonoBehaviour
{
    private static Action onShow;
    public Animator anim;
    public UnityAction onActive;

    private bool selected;

    private void Start()
    {
        onShow += Hide;
    }

    public void Show()
    {
        onShow?.Invoke();
        anim.SetBool("IsOver", true);
    }

    public void Hide()
    {
        anim.SetBool("IsOver", false);
        selected = false;
    }

    public void Select()
    {
        if (!selected)
        {
            onActive.Invoke();
            selected = true;
        }
    }
}
