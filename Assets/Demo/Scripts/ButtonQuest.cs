using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonQuest : MonoBehaviour
{
    Vector3 scale = Vector3.one * 1.1f;
    protected bool isQuestOpen = false;
    protected Image borderNotification;
    protected Action<GameObject, bool> onQuestPanelOpen = delegate { };
    private void Start()
    {
        borderNotification = transform.Find("Border/Notification").GetComponent<Image>();
        // Set New Quest Notification
        SetNewQuestNotification(borderNotification.gameObject);

        GetComponent<Button>()?.onClick.RemoveAllListeners();
        GetComponent<Button>()?.onClick.AddListener(() => ShowQuest(borderNotification.gameObject));

    }


    private void OnEnable() => onQuestPanelOpen += OpenQuestPanel;
    private void OnDisable() => onQuestPanelOpen -= OpenQuestPanel;

    protected void ShowQuest(GameObject goBorderNotification)
    {
        // Go back one more and find quest panel
        var questPanel = transform.Find("../Quest Panel/Content").gameObject;

        // If tweening runing return
        if (LeanTween.isTweening(questPanel))
            return;

        // Show
        LeanTween.moveLocalX(questPanel, isQuestOpen ? -160 : 150, 0.2f).setEaseLinear();
        // Is Quest Open
        isQuestOpen = !isQuestOpen;

        // Cancel if a player clicks on new quest
        onQuestPanelOpen?.Invoke(goBorderNotification, false);

    }
    protected void OpenQuestPanel(GameObject go, bool ac)
    {
        LeanTween.cancel(go);
        go.SetActive(ac);
    }
    protected void SetNewQuestNotification(GameObject goBorderNotification)
    {
        var imgBorder = goBorderNotification.GetComponent<Image>();
        var loopCount = Int16.MaxValue;

        LeanTween.scale(goBorderNotification, scale, 1).setOnStart(() => ChangeAlpha(imgBorder, loopCount)).setLoopCount(loopCount).setEaseLinear();
    }
    protected LTDescr ChangeAlpha(Image imgBorder, int loopCount) => LeanTween.alpha(imgBorder.rectTransform, 0f, 1f).setLoopCount(loopCount).setEaseLinear();

}
