using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle : MonoBehaviour
{
    public bool isToggleOff { get; protected set; }

    void Start()
    {
        var btn = GetComponent<Button>();

        btn?.onClick.RemoveAllListeners();
        btn?.onClick.AddListener(ToggleChange);
    }

    protected void ToggleChange()
    {
        var goDot = transform.Find("Dot").gameObject;
        // If tweening runing return
        if (LeanTween.isTweening(goDot))
            return;

        LeanTween.moveLocalX(goDot, isToggleOff ? -45f : 45f, 0.2f).setEaseInCirc();
        isToggleOff = !isToggleOff;

    }
}
