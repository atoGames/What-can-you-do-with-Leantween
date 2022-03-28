using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldCard : MonoBehaviour
{
    public float m_Speed = 0.2f;
    public LeanTweenType leanTweenType;
    protected Button btnWorldCard;
    private void OnEnable()
    {
        btnWorldCard = null ?? GetComponent<Button>();

        // Set world card button
        btnWorldCard?.onClick.RemoveAllListeners();
        btnWorldCard?.onClick.AddListener(RotateCard);
    }

    public void RotateCard()
    {
        // If tweening runing return
        if (LeanTween.isTweening(gameObject))
            return;

        // Check if the rotation is 0, and if not, change it to 180 . else 0
        var rot = Mathf.Round(transform.localRotation.y) == 0f ? 180f : 0f;

        LeanTween.rotateY(gameObject, rot, m_Speed).setOnStart(() => ScaleCard(gameObject, Vector3.one * 1.1f)).setOnComplete(() => ScaleCard(gameObject, Vector3.one)).setEase(leanTweenType);
        LeanTween.delayedCall(m_Speed / 2, () => ShowBack(rot)).setEase(leanTweenType);
    }

    //                                         It only works if the (back) game object is found
    protected void ShowBack(float rot) => transform.Find("Back")?.gameObject.SetActive(rot != 0);
    protected void ScaleCard(GameObject go, Vector3 scale) => LeanTween.scale(go, scale, m_Speed);

}
